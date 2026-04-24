using InventoryWarehouseSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryWarehouseSystem.Infrastructure.Persistence.Data;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Sku, sku =>
        {
            sku.Property(s => s.Value).HasColumnName("Sku").HasMaxLength(50).IsRequired();
            sku.HasIndex(x => x.Value).IsUnique();
        });

        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.ReorderLevel).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
