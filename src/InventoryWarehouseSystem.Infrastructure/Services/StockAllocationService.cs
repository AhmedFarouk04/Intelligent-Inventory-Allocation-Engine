using InventoryWarehouseSystem.Domain.Repositories;

namespace InventoryWarehouseSystem.Infrastructure.Services;

public class StockAllocationService
{
    private readonly IUnitOfWork _unitOfWork;

    public StockAllocationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> TryAllocateAsync(int productId, int warehouseId, int quantity, CancellationToken cancellationToken = default)
    {
        var stock = await _unitOfWork.Stocks.GetByProductAndWarehouseAsync(productId, warehouseId, cancellationToken);
        if (stock is null || stock.CurrentQuantity < quantity)
        {
            return false;
        }

        stock.Allocate(quantity);
        _unitOfWork.Stocks.Update(stock);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
