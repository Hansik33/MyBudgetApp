using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBudgetApp.Models;

namespace MyBudgetApp.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(transaction => transaction.Id);
            builder.Property(transaction => transaction.Id).HasColumnName("id");
            builder.Property(transaction => transaction.UserId).HasColumnName("user_id");
            builder.Property(transaction => transaction.CategoryId).HasColumnName("category_id");
            builder.Property(transaction => transaction.Amount).HasColumnName("amount");
            builder.Property(transaction => transaction.Type).HasColumnName("type").HasConversion<string>();
            builder.Property(transaction => transaction.Description).HasColumnName("description");
            builder.Property(transaction => transaction.PaymentMethod).HasColumnName("payment_method");
            builder.Property(transaction => transaction.Date).HasColumnName("date");
            builder.HasOne(transaction => transaction.Category).WithMany().HasForeignKey(transaction =>
            transaction.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}