using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Saving> Savings { get; set; }
    public DbSet<SavingGoal> SavingGoals { get; set; }
    public DbSet<Category> Categories { get; set; }
}
