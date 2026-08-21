using ClientHub.Models.Entities;

namespace ClientHub.Services
{
  public interface IClientService
  {
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client> CreateAsync(Client client);
    Task UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
    Task<List<PostalCode>> GetAllPostalCodesAsync();
  }
}