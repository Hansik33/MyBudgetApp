using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
