using ClientHub.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<UserModel> Users { get; set; }
    public DbSet<ClientModel> Clients { get; set; }
    public DbSet<PostalCodeModel> PostalCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserModel>().ToTable("Users");
      modelBuilder.Entity<ClientModel>().ToTable("Clients");
      modelBuilder.Entity<PostalCodeModel>().ToTable("PostalCodes");
    }
  }
}