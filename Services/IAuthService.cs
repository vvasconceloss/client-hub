using ClientHub.Models.Entities;

namespace ClientHub.Services
{
  public interface IAuthService
  {
    Task<User?> RegisterAsync(string email, string password);
    Task<User?> ValidateCredentialsAsync(string email, string password);
  }
}