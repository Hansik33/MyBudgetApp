using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data.Configurations
{
    public class SavingGoalConfiguration : IEntityTypeConfiguration<SavingGoal>
    {
        public void Configure(EntityTypeBuilder<SavingGoal> builder)
        {
            builder.ToTable("savinggoals");
            builder.HasKey(savingGoal => savingGoal.Id);
            builder.Property(savingGoal => savingGoal.Id).HasColumnName("id");
            builder.Property(savingGoal => savingGoal.UserId).HasColumnName("user_id");
            builder.Property(savingGoal => savingGoal.Name).HasColumnName("name");
            builder.Property(savingGoal => savingGoal.TargetAmount).HasColumnName("target_amount");
            builder.Property(savingGoal => savingGoal.SavedAmount).HasColumnName("saved_amount");
            builder.Property(savingGoal => savingGoal.Deadline).HasColumnName("deadline");
        }
    }
}