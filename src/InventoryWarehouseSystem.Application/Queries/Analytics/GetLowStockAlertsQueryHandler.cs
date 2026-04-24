using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Application.Mappings;
using InventoryWarehouseSystem.Domain.Repositories;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Analytics;

public class GetLowStockAlertsQueryHandler : IRequestHandler<GetLowStockAlertsQuery, IReadOnlyCollection<ProductDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLowStockAlertsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<ProductDTO>> Handle(GetLowStockAlertsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        var stocks = await _unitOfWork.Stocks.GetAllAsync(cancellationToken);

        var lowStockIds = stocks
            .GroupBy(x => x.ProductId)
            .Where(group => group.Sum(s => s.CurrentQuantity) <= products.First(p => p.Id == group.Key).ReorderLevel)
            .Select(group => group.Key)
            .ToHashSet();

        var result = products.Where(p => lowStockIds.Contains(p.Id)).ToList();
        return result.Select(x => x.ToDto()).ToArray();
    }
}
