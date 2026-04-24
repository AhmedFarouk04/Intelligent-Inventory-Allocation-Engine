using InventoryWarehouseSystem.Domain.Enums;
using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.Domain.ValueObjects;
using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.Entities;

public class Product : AggregateRoot
{
    public Sku Sku { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int CategoryId { get; private set; }
    public int ReorderLevel { get; private set; }
    public ProductStatus Status { get; private set; } = ProductStatus.Active;

    public Category? Category { get; set; }
    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    protected Product()
    {
    }

    public static Product Create(string sku, string name, int categoryId, int reorderLevel, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.");
        }

        if (reorderLevel < 0)
        {
            throw new ArgumentException("Reorder level cannot be negative.");
        }

        var product = new Product
        {
            Sku = Sku.Create(sku),
            Name = name.Trim(),
            CategoryId = categoryId,
            ReorderLevel = reorderLevel,
            Description = description,
            Status = ProductStatus.Active
        };

        product.RaiseDomainEvent(new ProductCreatedEvent { ProductId = product.Id, Sku = product.Sku.Value });
        return product;
    }

    public void UpdateReorderLevel(int newLevel)
    {
        if (newLevel < 0)
        {
            throw new ArgumentException("Reorder level cannot be negative.");
        }

        ReorderLevel = newLevel;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = ProductStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }
}
