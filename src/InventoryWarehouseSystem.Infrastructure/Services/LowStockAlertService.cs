using InventoryWarehouseSystem.Domain.Repositories;

namespace InventoryWarehouseSystem.Infrastructure.Services;

public class LowStockAlertService
{
    private readonly IUnitOfWork _unitOfWork;

    public LowStockAlertService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<int>> GetLowStockProductIdsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        var stocks = await _unitOfWork.Stocks.GetAllAsync(cancellationToken);

        return products
            .Where(p => stocks.Where(s => s.ProductId == p.Id).Sum(s => s.CurrentQuantity) <= p.ReorderLevel)
            .Select(p => p.Id)
            .ToArray();
    }
}
