using ClientHub.Data;
using ClientHub.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Services
{
  public class ClientService(ApplicationDbContext db) : IClientService
  {
    public async Task<List<Client>> GetAllAsync()
    {
      return await db.Clients
        .Include(c => c.PostalCode)
        .OrderBy(c => c.LastName)
        .ThenBy(c => c.FirstName)
        .ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
      return await db.Clients
        .Include(c => c.PostalCode)
        .FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<bool> DeleteAsync(int id)
    {
      var client = await db.Clients.FindAsync(id);

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