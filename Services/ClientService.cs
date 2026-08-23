using ClientHub.Data;
using ClientHub.Models.Entities;
using ClientHub.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Services
{
  public class ClientService(ApplicationDbContext db) : IClientService
  {
    public async Task<PagedResult<Client>> GetAllAsync(Guid userId, string? search, int page, int pageSize)
    {
      IQueryable<Client> query = db.Clients
        .Include(c => c.PostalCode)
        .Where(c => c.CreatedByUserId == userId);

      if (!string.IsNullOrWhiteSpace(search))
      {
        query = query.Where(c =>
          c.FirstName.Contains(search) ||
          c.LastName.Contains(search) ||
          c.Email.Contains(search));
      }

      int totalItems = await query.CountAsync();

      var items = await query
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return new PagedResult<Client>
      {
        Items = items,
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems
      };
    }

    public async Task<Client?> GetByIdAsync(int id, Guid userId)
    {
      return await db.Clients
        .Include(c => c.PostalCode)
        .FirstOrDefaultAsync(c => c.Id == id && c.CreatedByUserId == userId);
    }

    public async Task<Client> CreateAsync(Client client)
    {
      db.Clients.Add(client);
      await db.SaveChangesAsync();

      return client;
    }

    public async Task UpdateAsync(Client client)
    {
      await db.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id, Guid userId)
    {
      var client = await db.Clients
        .FirstOrDefaultAsync(c => c.Id == id && c.CreatedByUserId == userId);

      if (client is null)
      {
        return false;
      }

      db.Clients.Remove(client);
      await db.SaveChangesAsync();

      return true;
    }

    public async Task<List<PostalCode>> GetAllPostalCodesAsync()
    {
      return await db.PostalCodes
        .OrderBy(p => p.City)
        .ToListAsync();
    }
  }
}