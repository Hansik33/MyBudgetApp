using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data.Configurations
{
    public class SavingConfiguration : IEntityTypeConfiguration<Saving>
    {
        public void Configure(EntityTypeBuilder<Saving> builder)
        {
            builder.ToTable("savings");
            builder.HasKey(saving => saving.Id);
            builder.Property(saving => saving.Id).HasColumnName("id");
            builder.Property(saving => saving.UserId).HasColumnName("user_id");
            builder.Property(saving => saving.GoalId).HasColumnName("goal_id");
            builder.Property(saving => saving.Amount).HasColumnName("amount");
            builder.Property(saving => saving.Date).HasColumnName("date");
        }
    }
}