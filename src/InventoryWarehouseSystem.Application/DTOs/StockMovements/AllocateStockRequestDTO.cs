namespace InventoryWarehouseSystem.Application.DTOs.StockMovements;

public class AllocateStockRequestDTO
{
    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Quantity { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Notes { get; set; }
    public string? MovedBy { get; set; }
}
