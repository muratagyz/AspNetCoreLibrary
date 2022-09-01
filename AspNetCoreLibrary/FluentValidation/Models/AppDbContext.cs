using Microsoft.EntityFrameworkCore;

namespace FluentValidation.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
}