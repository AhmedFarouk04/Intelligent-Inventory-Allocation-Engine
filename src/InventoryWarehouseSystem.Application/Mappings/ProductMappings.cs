using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Domain.Entities;

namespace InventoryWarehouseSystem.Application.Mappings;

public static class ProductMappings
{
    public static ProductDTO ToDto(this Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Sku = product.Sku.Value,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            ReorderLevel = product.ReorderLevel,
            Status = product.Status.ToString()
        };
    }
}
