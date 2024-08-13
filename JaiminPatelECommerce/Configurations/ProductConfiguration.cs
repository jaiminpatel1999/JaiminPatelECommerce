using JaiminPatelECommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JaiminPatelECommerce.Configurations
{

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Define the table name
            builder.ToTable("Products");

            // Define the primary key
            builder.HasKey(p => p.ProductId);

            // Define properties
            builder.Property(p => p.ProductId)
                   .ValueGeneratedOnAdd(); // Auto-incremented ID

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Maximum length of 100 characters

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define the decimal precision

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(255); // Maximum length of 255 characters

            builder.Property(p => p.Stock)
                   .IsRequired();

            builder.Property(p => p.IsAvailable)
                   .IsRequired();

            builder.HasIndex(p => p.Name).HasDatabaseName("IX_Product_Name");
        }
    }
}
