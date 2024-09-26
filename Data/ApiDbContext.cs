using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        
    }

    public DbSet<Driver> Drivers { get;}
}