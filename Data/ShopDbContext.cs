using GenchoFit.Models;
using Microsoft.EntityFrameworkCore;

namespace GenchoFit.Data;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
}
