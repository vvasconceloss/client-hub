using System.Diagnostics;
using ClientHub.Extensions;
using ClientHub.Models;
using ClientHub.Models.ViewModels;
using ClientHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientHub.Controllers
{
  public class HomeController : Controller
  {
    private readonly IClientService _clientService;

    public HomeController(IClientService clientService)
    {
      _clientService = clientService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
      var model = await _clientService.GetDashboardAsync(User.GetUserId());

      return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Status(int id)
    {
      Response.StatusCode = id;

      return View(id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}