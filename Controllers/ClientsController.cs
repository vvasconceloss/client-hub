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
      await PopulatePostalCodeOptions(model);

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateClientViewModel model)
    {
      if (!ModelState.IsValid)
      {
        await PopulatePostalCodeOptions(model);
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

    private async Task PopulatePostalCodeOptions(CreateClientViewModel model)
    {
      var postalCodes = await _db.PostalCodes
        .OrderBy(p => p.City)
        .ToListAsync();

      model.PostalCodeOptions = postalCodes
        .Select(p => new SelectListItem
        {
          Value = p.Id.ToString(),
          Text = p.Code + " - " + p.City
        })
        .ToList();
    }
  }
}
