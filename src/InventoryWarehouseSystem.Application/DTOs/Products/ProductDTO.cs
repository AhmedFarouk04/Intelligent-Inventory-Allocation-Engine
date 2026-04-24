namespace InventoryWarehouseSystem.Application.DTOs.Products;

public class ProductDTO
{
    public int Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public int ReorderLevel { get; set; }
    public string Status { get; set; } = string.Empty;
}
