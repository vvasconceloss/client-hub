using ClientHub.Models.Entities;

namespace ClientHub.Services
{
  public interface IClientService
  {
    Task<List<Client>> GetAllAsync(Guid userId, string? search);
    Task<Client?> GetByIdAsync(int id, Guid userId);
    Task<Client> CreateAsync(Client client);
    Task UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id, Guid userId);
    Task<List<PostalCode>> GetAllPostalCodesAsync();
  }
}