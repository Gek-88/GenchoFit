using GenchoFit.Models;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Data;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(product => product.Id);
            entity.Property(product => product.Name).IsRequired().HasMaxLength(100);
            entity.Property(product => product.Category).IsRequired().HasMaxLength(50);
            entity.Property(product => product.Price).HasPrecision(18, 2);
            entity.Property(product => product.Description).IsRequired().HasMaxLength(500);
            entity.Property(product => product.Emoji).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.HasIndex(user => user.Email).IsUnique();
            entity.Property(user => user.Email).IsRequired().HasMaxLength(160);
            entity.Property(user => user.Name).IsRequired().HasMaxLength(80);
            entity.Property(user => user.PasswordHash).IsRequired().HasMaxLength(255);
            entity.Property(user => user.CreatedAt).IsRequired();
        });
    }
}
