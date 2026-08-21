using ClientHub.Data;
using ClientHub.Models.Entities;
using ClientHub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Controllers
{
  public class ClientsController : Controller
  {
    private readonly ApplicationDbContext _db;

    public ClientsController(ApplicationDbContext db)
    {
      _db = db;
    }

    public async Task<IActionResult> Index()
    {
      var clients = await _db.Clients
        .Include(c => c.PostalCode)
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .Select(c => new ClientListItemViewModel
        {
          Id = c.Id,
          FullName = c.FirstName + " " + c.LastName,
          Email = c.Email,
          Phone = c.Phone,
          PostalCode = c.PostalCode.Code
        })
        .ToListAsync();

      return View(clients);
    }

    public async Task<IActionResult> Details(int id)
    {
      var client = await _db.Clients
        .Include(c => c.PostalCode)
        .Where(c => c.Id == id)
        .Select(c => new ClientDetailsViewModel
        {
          Id = c.Id,
          FullName = c.FirstName + " " + c.LastName,
          Email = c.Email,
          Phone = c.Phone,
          Address = c.Address,
          PostalCode = c.PostalCode.Code,
          City = c.PostalCode.City,
          CreatedAt = c.CreatedAt
        })
        .FirstOrDefaultAsync();

      if (client is null)
      {
        return NotFound();
      }

      return View(client);
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
        CreatedByUserId = SeedData.DemoUserId,
        CreatedAt = DateTime.UtcNow
      };

      _db.Clients.Add(client);
      await _db.SaveChangesAsync();

      return RedirectToAction(nameof(Details), new { id = client.Id });
    }

    public async Task<IActionResult> Edit(int id)
    {
      var client = await _db.Clients.FindAsync(id);

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

      var client = await _db.Clients.FindAsync(id);

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

      await _db.SaveChangesAsync();

      return RedirectToAction(nameof(Details), new { id = client.Id });
    }

    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var client = await _db.Clients
        .Include(c => c.PostalCode)
        .Where(c => c.Id == id)
        .Select(c => new ClientDetailsViewModel
        {
          Id = c.Id,
          FullName = c.FirstName + " " + c.LastName,
          Email = c.Email,
          Phone = c.Phone,
          Address = c.Address,
          PostalCode = c.PostalCode.Code,
          City = c.PostalCode.City,
          CreatedAt = c.CreatedAt
        })
        .FirstOrDefaultAsync();

      if (client is null)
      {
        return NotFound();
      }

      return View(client);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
    {
      var client = await _db.Clients.FindAsync(id);

      if (client is null)
      {
        return NotFound();
      }

      _db.Clients.Remove(client);
      await _db.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }

    private async Task<List<SelectListItem>> GetPostalCodeOptions()
    {
      return await _db.PostalCodes
        .OrderBy(p => p.City)
        .Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = p.Code + " - " + p.City
        })
        .ToListAsync();
    }
  }
}
