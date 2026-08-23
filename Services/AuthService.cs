using ClientHub.Data;
using ClientHub.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Services
{
  public class AuthService(ApplicationDbContext db) : IAuthService
  {
    private readonly PasswordHasher<User> _passwordHasher = new();

    public async Task<User?> RegisterAsync(string email, string password)
    {
      if (await db.Users.AnyAsync(u => u.Email == email))
      {
        return null;
      }

      var user = new User
      {
        Id = Guid.NewGuid(),
        Email = email,
        PasswordHash = _passwordHasher.HashPassword(null!, password),
        CreatedAt = DateTime.UtcNow
      };

      db.Users.Add(user);
      await db.SaveChangesAsync();

      return user;
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
      var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

      if (user is null)
      {
        return null;
      }

      var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

      return result == PasswordVerificationResult.Success ? user : null;
    }
  }
}