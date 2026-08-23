using System.Security.Claims;
using ClientHub.Models.Entities;
using ClientHub.Models.ViewModels;
using ClientHub.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ClientHub.Controllers
{
  public class AccountController : Controller
  {
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
      _authService = authService;
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var user = await _authService.RegisterAsync(model.Email, model.Password);

      if (user is null)
      {
        ModelState.AddModelError(nameof(model.Email), "An account with this email already exists.");
        return View(model);
      }

      await SignInAsync(user, isPersistent: false);

      return RedirectToAction("Index", "Home");
    }

    public IActionResult Login(string? returnUrl)
    {
      var model = new LoginViewModel { ReturnUrl = returnUrl };

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var user = await _authService.ValidateCredentialsAsync(model.Email, model.Password);

      if (user is null)
      {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(model);
      }

      await SignInAsync(user, isPersistent: model.RememberMe);

      if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
      {
        return Redirect(model.ReturnUrl);
      }

      return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction("Index", "Home");
    }

    private async Task SignInAsync(User user, bool isPersistent)
    {
      var claims = new List<Claim>
      {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Name, user.Email)
      };

      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);

      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        principal,
        new AuthenticationProperties { IsPersistent = isPersistent });
    }
  }
}