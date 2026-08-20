using ClientHub.Models.Entities;

namespace ClientHub.Data
{
  public static class SeedData
  {
    private static readonly Guid DemoUserId = Guid.Parse("11111111-2222-3333-4444-555555555555");

    public static readonly PostalCode[] PostalCodes =
    [
      new() { Id = 1, Code = "1000-001", City = "Lisboa" },
      new() { Id = 2, Code = "4000-100", City = "Porto" },
      new() { Id = 3, Code = "3000-000", City = "Coimbra" },
      new() { Id = 4, Code = "4700-000", City = "Braga" },
      new() { Id = 5, Code = "8000-000", City = "Faro" },
      new() { Id = 6, Code = "3800-000", City = "Aveiro" },
      new() { Id = 7, Code = "7000-000", City = "Évora" },
      new() { Id = 8, Code = "2900-000", City = "Setúbal" },
      new() { Id = 9, Code = "9000-000", City = "Funchal" },
      new() { Id = 10, Code = "9500-000", City = "Ponta Delgada" }
    ];

    public static readonly User User = new()
    {
      Id = DemoUserId,
      Email = "demo@clienthub.dev",
      PasswordHash = "AQAAAAIAAYagAAAAEDurHBiNUlg+IflHWRfQNTbsgriWYFY65X9z6wjkLpc7jTv+nqk4yZTtATQR6XNdVA==",
      CreatedAt = new DateTime(2026, 8, 1, 0, 0, 0)
    };

    public static readonly Client[] Clients =
    [
      new()
      {
        Id = 1,
        FirstName = "John",
        LastName = "Smith",
        Email = "john.smith@example.com",
        Phone = "+351 912 345 001",
        Address = "Rua Augusta, 100",
        PostalCodeId = 1,
        CreatedByUserId = DemoUserId,
        CreatedAt = new DateTime(2026, 8, 15, 9, 0, 0)
      },
      new()
      {
        Id = 2,
        FirstName = "Maria",
        LastName = "Silva",
        Email = "maria.silva@example.com",
        Phone = "+351 965 432 109",
        Address = "Avenida da Liberdade, 200",
        PostalCodeId = 2,
        CreatedByUserId = DemoUserId,
        CreatedAt = new DateTime(2026, 8, 14, 14, 30, 0)
      },
      new()
      {
        Id = 3,
        FirstName = "Pedro",
        LastName = "Santos",
        Email = "pedro.santos@example.com",
        Phone = "+351 961 234 567",
        Address = "Rua Ferreira Borges, 300",
        PostalCodeId = 3,
        CreatedByUserId = DemoUserId,
        CreatedAt = new DateTime(2026, 8, 12, 11, 15, 0)
      },
      new()
      {
        Id = 4,
        FirstName = "Ana",
        LastName = "Souza",
        Email = "ana.souza@example.com",
        Phone = "+351 917 654 321",
        Address = "Praça do Município, 400",
        PostalCodeId = 4,
        CreatedByUserId = DemoUserId,
        CreatedAt = new DateTime(2026, 8, 10, 16, 45, 0)
      },
      new()
      {
        Id = 5,
        FirstName = "Carlos",
        LastName = "Oliveira",
        Email = "carlos.oliveira@example.com",
        Phone = "+351 969 876 543",
        PostalCodeId = 5,
        CreatedByUserId = DemoUserId,
        CreatedAt = new DateTime(2026, 8, 8, 10, 0, 0)
      }
    ];
  }
}