using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data.Configurations
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.ToTable("budgets");
            builder.HasKey(budget => budget.Id);
            builder.Property(budget => budget.Id).HasColumnName("id");
            builder.Property(budget => budget.UserId).HasColumnName("user_id");
            builder.Property(budget => budget.CategoryId).HasColumnName("category_id");
            builder.Property(budget => budget.MonthNumber).HasColumnName("month_number");
            builder.Property(budget => budget.Year).HasColumnName("year");
            builder.Property(budget => budget.LimitAmount).HasColumnName("limit_amount");
            builder.HasOne(budget => budget.Category).WithMany().HasForeignKey(budget =>
            budget.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}