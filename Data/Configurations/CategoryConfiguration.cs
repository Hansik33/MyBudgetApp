using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBudgetApp.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(categorie => categorie.Id);
            builder.Property(categorie => categorie.Id).HasColumnName("id");
            builder.Property(categorie => categorie.Name).HasColumnName("name");
        }
    }
}