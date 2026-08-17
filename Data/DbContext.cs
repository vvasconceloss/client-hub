using ClientHub.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientHub.Data
{
  public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<PostalCode> PostalCodes { get; set; }

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
    }
  }
}