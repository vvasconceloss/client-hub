using System.Security.Claims;
using ClientHub.Models.Entities;
using ClientHub.Models.ViewModels;
using ClientHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientHub.Controllers
{
  [Authorize]
  public class ClientsController : Controller
  {
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
      _clientService = clientService;
    }

    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<IActionResult> Index(string? search)
    {
      var clients = await _clientService.GetAllAsync(CurrentUserId, search);

      ViewData["Search"] = search;

      var model = clients.Select(c => new ClientListItemViewModel
      {
        Id = c.Id,
        FullName = c.FirstName + " " + c.LastName,
        Email = c.Email,
        Phone = c.Phone,
        PostalCode = c.PostalCode.Code
      }).ToList();

      return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
      var client = await _clientService.GetByIdAsync(id, CurrentUserId);

      if (client is null)
      {
        return NotFound();
      }

      var model = new ClientDetailsViewModel
      {
        Id = client.Id,
        FullName = client.FirstName + " " + client.LastName,
        Email = client.Email,
        Phone = client.Phone,
        Address = client.Address,
        PostalCode = client.PostalCode.Code,
        City = client.PostalCode.City,
        CreatedAt = client.CreatedAt
      };

      return View(model);
    }

    public async Task<IActionResult> Create()
    {
      var model = new CreateClientViewModel();
      model.PostalCodeOptions = await GetPostalCodeOptions();

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateClientViewModel model)
    {
      if (!ModelState.IsValid)
      {
        model.PostalCodeOptions = await GetPostalCodeOptions();
        return View(model);
      }

      var client = new Client
      {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        Phone = model.Phone,
        Address = model.Address,
        PostalCodeId = model.PostalCodeId!.Value,
        CreatedByUserId = CurrentUserId,
        CreatedAt = DateTime.UtcNow
      };

      await _clientService.CreateAsync(client);

      return RedirectToAction(nameof(Details), new { id = client.Id });
    }

    public async Task<IActionResult> Edit(int id)
    {
      var client = await _clientService.GetByIdAsync(id, CurrentUserId);

      if (client is null)
      {
        return NotFound();
      }

      ViewData["ClientId"] = id;

      var model = new EditClientViewModel
      {
        FirstName = client.FirstName,
        LastName = client.LastName,
        Email = client.Email,
        Phone = client.Phone,
        Address = client.Address,
        PostalCodeId = client.PostalCodeId
      };
      model.PostalCodeOptions = await GetPostalCodeOptions();

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, EditClientViewModel model)
    {
      if (!ModelState.IsValid)
      {
        ViewData["ClientId"] = id;
        model.PostalCodeOptions = await GetPostalCodeOptions();
        return View(model);
      }

      var client = await _clientService.GetByIdAsync(id, CurrentUserId);

      if (client is null)
      {
        return NotFound();
      }

      client.FirstName = model.FirstName;
      client.LastName = model.LastName;
      client.Email = model.Email;
      client.Phone = model.Phone;
      client.Address = model.Address;
      client.PostalCodeId = model.PostalCodeId!.Value;
      client.UpdatedAt = DateTime.UtcNow;

      await _clientService.UpdateAsync(client);

      return RedirectToAction(nameof(Details), new { id = client.Id });
    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var client = await _clientService.GetByIdAsync(id, CurrentUserId);

      if (client is null)
      {
        return NotFound();
      }

      var model = new ClientDetailsViewModel
      {
        Id = client.Id,
        FullName = client.FirstName + " " + client.LastName,
        Email = client.Email,
        Phone = client.Phone,
        Address = client.Address,
        PostalCode = client.PostalCode.Code,
        City = client.PostalCode.City,
        CreatedAt = client.CreatedAt
      };

      return View(model);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
    {
      if (!await _clientService.DeleteAsync(id, CurrentUserId))
      {
        return NotFound();
      }

      return RedirectToAction(nameof(Index));
    }

    private async Task<List<SelectListItem>> GetPostalCodeOptions()
    {
      var postalCodes = await _clientService.GetAllPostalCodesAsync();

      return postalCodes
        .Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = p.Code + " - " + p.City
        })
        .ToList();
    }
  }
}