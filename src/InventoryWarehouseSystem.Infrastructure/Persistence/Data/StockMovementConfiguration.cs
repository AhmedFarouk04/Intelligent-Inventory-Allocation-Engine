using InventoryWarehouseSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryWarehouseSystem.Infrastructure.Persistence.Data;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements", tableBuilder =>
        {
            tableBuilder.HasCheckConstraint("CK_StockMovement_Quantity", "[Quantity] > 0");
        });
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);
        builder.Property(x => x.ReferenceNumber).HasMaxLength(100);
        builder.Property(x => x.MovedBy).HasMaxLength(100);
        builder.Property(x => x.MovedAt).IsRequired();

        builder.HasIndex(x => new { x.ProductId, x.WarehouseId, x.MovedAt });
        builder.HasIndex(x => new { x.WarehouseId, x.MovedAt });
        builder.HasOne(x => x.Product)
            .WithMany(x => x.StockMovements)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
