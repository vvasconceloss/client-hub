using ClientHub.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    required public DbSet<User> Users { get; set; }
    required public DbSet<Client> Clients { get; set; }
    required public DbSet<PostalCode> PostalCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("Users");
      modelBuilder.Entity<Client>().ToTable("Clients");
      modelBuilder.Entity<PostalCode>().ToTable("PostalCodes");

      modelBuilder.Entity<Client>()
        .HasOne(c => c.PostalCode)
        .WithMany(p => p.Clients)
        .HasForeignKey(c => c.PostalCodeId)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Client>()
        .HasOne(c => c.CreatedByUser)
        .WithMany()
        .HasForeignKey(c => c.CreatedByUserId)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<PostalCode>().HasData(SeedData.PostalCodes);
      modelBuilder.Entity<User>().HasData(SeedData.User);
      modelBuilder.Entity<Client>().HasData(SeedData.Clients);
    }
  }
}