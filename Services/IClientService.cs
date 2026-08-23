using ClientHub.Models.Entities;
using ClientHub.Models.ViewModels;

namespace ClientHub.Services
{
  public interface IClientService
  {
    Task<PagedResult<Client>> GetAllAsync(Guid userId, string? search, int? postalCodeId, string? sortBy, string? sortDir, int page, int pageSize);
    Task<Client?> GetByIdAsync(int id, Guid userId);
    Task<Client> CreateAsync(Client client);
    Task UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id, Guid userId);
    Task<List<PostalCode>> GetAllPostalCodesAsync();
  }
}