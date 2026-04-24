namespace InventoryWarehouseSystem.Application.Exceptions;

public class InsufficientStockException : Exception
{
    public InsufficientStockException(int productId, int warehouseId, int requested, int available)
        : base($"Insufficient stock for Product:{productId} Warehouse:{warehouseId}. Requested={requested}, Available={available}")
    {
    }
}
