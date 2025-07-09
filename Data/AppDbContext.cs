using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<SavingGoal> SavingGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.Username).HasColumnName("username");
                entity.Property(entity => entity.Password_hash).HasColumnName("password_hash");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("budgets");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.UserId).HasColumnName("user_id");
                entity.Property(entity => entity.CategoryId).HasColumnName("category_id");
                entity.Property(entity => entity.MonthNumber).HasColumnName("month_number");
                entity.Property(entity => entity.Month).HasColumnName("month");
                entity.Property(entity => entity.LimitAmount).HasColumnName("limit_amount");
                entity.Ignore(entity => entity.CategoryName);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.UserId).HasColumnName("user_id");
                entity.Property(entity => entity.CategoryId).HasColumnName("category_id");
                entity.Property(entity => entity    .Amount).HasColumnName("amount");
                entity.Property(entity => entity.Type).HasColumnName("type");
                entity.Property(entity => entity.Description).HasColumnName("description");
                entity.Property(entity => entity.PaymentMethod).HasColumnName("payment_method");
                entity.Property(entity => entity.Date).HasColumnName("date");
                entity.Ignore(entity => entity.CategoryName);
            });

            modelBuilder.Entity<Saving>(entity =>
            {
                entity.ToTable("savings");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.UserId).HasColumnName("user_id");
                entity.Property(entity => entity.GoalId).HasColumnName("goal_id");
                entity.Property(entity => entity.Amount).HasColumnName("amount");
                entity.Property(entity => entity.Date).HasColumnName("date");
            });

            modelBuilder.Entity<SavingGoal>(entity =>
            {
                entity.ToTable("savinggoals");
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Id).HasColumnName("id");
                entity.Property(entity => entity.UserId).HasColumnName("user_id");
                entity.Property(entity => entity.Name).HasColumnName("name");
                entity.Property(entity => entity.TargetAmount).HasColumnName("target_amount");
                entity.Property(entity => entity.SavedAmount).HasColumnName("saved_amount");
                entity.Property(entity => entity.Deadline).HasColumnName("deadline");
            });
        }
    }
}