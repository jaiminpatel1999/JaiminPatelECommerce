using JaiminPatelECommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JaiminPatelECommerce.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Define the table name
            builder.ToTable("Orders");

            // Define the primary key
            builder.HasKey(o => o.OrderId);

            // Define properties
            builder.Property(o => o.OrderId)
                   .ValueGeneratedOnAdd(); // Auto-incremented ID

            builder.Property(o => o.UserId)
                   .IsRequired(); // UserId is required

            builder.Property(o => o.OrderDate)
                   .IsRequired()
                   .HasColumnType("datetime"); // Ensure OrderDate is stored as datetime

            builder.Property(o => o.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define the decimal precision for TotalAmount

            // Define relationships
            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete to remove OrderItems when an Order is deleted

            builder.HasIndex(o => o.UserId).HasDatabaseName("IX_Order_UserId");
            builder.HasIndex(o => o.OrderDate).HasDatabaseName("IX_Order_OrderDate");
        }
    }
}
