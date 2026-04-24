namespace InventoryWarehouseSystem.Application.DTOs.Products;

public class CreateProductDTO
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public int ReorderLevel { get; set; }
}
