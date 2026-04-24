using InventoryWarehouseSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryWarehouseSystem.Infrastructure.Persistence.Data;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stocks", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint("CK_Stock_CurrentQuantity", "[CurrentQuantity] >= 0");
            tableBuilder.HasCheckConstraint("CK_Stock_ReservedQuantity", "[ReservedQuantity] >= 0");
        });
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CurrentQuantity).IsRequired();
        builder.Property(x => x.ReservedQuantity).IsRequired();
        builder.HasIndex(x => new { x.ProductId, x.WarehouseId }).IsUnique();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Stocks)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Warehouse)
            .WithMany(x => x.Stocks)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
