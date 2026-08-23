using ClientHub.Data;
using ClientHub.Models.Entities;
using ClientHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace ClientHub.Tests
{
  public class ClientServiceTests
  {
    private static ApplicationDbContext CreateContext(string databaseName)
    {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName)
        .Options;

      return new ApplicationDbContext(options);
    }

    private static Client CreateClient(Guid userId, string lastName = "Silva", int postalCodeId = 1)
    {
      return new Client
      {
        FirstName = "Maria",
        LastName = lastName,
        Email = lastName.ToLowerInvariant() + "@example.com",
        Phone = "+351 900 000 000",
        PostalCodeId = postalCodeId,
        CreatedByUserId = userId,
        CreatedAt = DateTime.UtcNow
      };
    }

    private static async Task SeedPostalCodeAsync(ApplicationDbContext db, int id = 1)
    {
      db.PostalCodes.Add(new PostalCode { Id = id, Code = "1000-001", City = "Lisboa" });
      await db.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateClient()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new ClientService(db, NullLogger<ClientService>.Instance);
      var userId = Guid.NewGuid();

      var created = await service.CreateAsync(CreateClient(userId));

      using var assertDb = CreateContext(dbName);
      var saved = await assertDb.Clients.FindAsync(created.Id);
      Assert.NotNull(saved);
      Assert.Equal(created.FirstName, saved!.FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnClient()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new ClientService(db, NullLogger<ClientService>.Instance);
      var userId = Guid.NewGuid();
      await SeedPostalCodeAsync(db);
      var client = CreateClient(userId);
      db.Clients.Add(client);
      await db.SaveChangesAsync();

      var result = await service.GetByIdAsync(client.Id, userId);

      Assert.NotNull(result);
      Assert.Equal(client.Id, result!.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenClientDoesNotExist()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new ClientService(db, NullLogger<ClientService>.Instance);

      var result = await service.GetByIdAsync(999, Guid.NewGuid());

      Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteClient()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new ClientService(db, NullLogger<ClientService>.Instance);
      var userId = Guid.NewGuid();
      var client = CreateClient(userId);
      db.Clients.Add(client);
      await db.SaveChangesAsync();

      var deleted = await service.DeleteAsync(client.Id, userId);

      Assert.True(deleted);
      using var assertDb = CreateContext(dbName);
      Assert.Null(await assertDb.Clients.FindAsync(client.Id));
    }

    [Fact]
    public async Task GetAllAsync_ShouldFilterByUser()
    {
      var dbName = Guid.NewGuid().ToString();
      var db = CreateContext(dbName);
      var service = new ClientService(db, NullLogger<ClientService>.Instance);
      var userA = Guid.NewGuid();
      var userB = Guid.NewGuid();
      await SeedPostalCodeAsync(db);
      db.Clients.AddRange(
        CreateClient(userA, "Souza"),
        CreateClient(userA, "Oliveira"),
        CreateClient(userB, "Santos"));
      await db.SaveChangesAsync();

      var result = await service.GetAllAsync(userA, null, null, null, null, 1, 10);

      Assert.Equal(2, result.TotalItems);
      Assert.All(result.Items, c => Assert.Equal(userA, c.CreatedByUserId));
    }
  }
}
