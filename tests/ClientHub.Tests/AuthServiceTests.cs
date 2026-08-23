using ClientHub.Data;
using ClientHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClientHub.Tests
{
  public class AuthServiceTests
  {
    private static ApplicationDbContext CreateContext(string databaseName)
    {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName)
        .Options;

      return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUserWithHashedPassword()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new AuthService(db, NullLogger<AuthService>.Instance);

      var user = await service.RegisterAsync("demo@example.com", "ClientHub123!");

      Assert.NotNull(user);
      Assert.NotEqual("ClientHub123!", user!.PasswordHash);

      using var assertDb = CreateContext(dbName);
      Assert.NotNull(await assertDb.Users.FirstOrDefaultAsync(u => u.Email == "demo@example.com"));
    }

    [Fact]
    public async Task RegisterAsync_ShouldRejectDuplicateEmail()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new AuthService(db, NullLogger<AuthService>.Instance);

      await service.RegisterAsync("demo@example.com", "ClientHub123!");
      var duplicate = await service.RegisterAsync("demo@example.com", "OutraPassword");

      Assert.Null(duplicate);
      using var assertDb = CreateContext(dbName);
      Assert.Equal(1, await assertDb.Users.CountAsync());
    }

    [Fact]
    public async Task ValidateCredentialsAsync_ShouldReturnUser_WithValidCredentials()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new AuthService(db, NullLogger<AuthService>.Instance);
      await service.RegisterAsync("demo@example.com", "ClientHub123!");

      var user = await service.ValidateCredentialsAsync("demo@example.com", "ClientHub123!");

      Assert.NotNull(user);
    }

    [Fact]
    public async Task ValidateCredentialsAsync_ShouldReturnNull_WithInvalidPassword()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new AuthService(db, NullLogger<AuthService>.Instance);
      await service.RegisterAsync("demo@example.com", "ClientHub123!");

      var user = await service.ValidateCredentialsAsync("demo@example.com", "wrong-password");

      Assert.Null(user);
    }

    [Fact]
    public async Task ValidateCredentialsAsync_ShouldReturnNull_WhenEmailDoesNotExist()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new AuthService(db, NullLogger<AuthService>.Instance);

      var user = await service.ValidateCredentialsAsync("nobody@example.com", "ClientHub123!");

      Assert.Null(user);
    }
  }
}