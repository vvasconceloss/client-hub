using ClientHub.Data;
using ClientHub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
  }
}
