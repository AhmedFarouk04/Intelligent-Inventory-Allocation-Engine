using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Repositories;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.StockMovements;

public class GetMovementHistoryQueryHandler : IRequestHandler<GetMovementHistoryQuery, IReadOnlyCollection<StockMovement>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMovementHistoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<StockMovement>> Handle(GetMovementHistoryQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.StockMovements.GetHistoryAsync(request.ProductId, request.WarehouseId, request.From, request.To, cancellationToken);
        return data;
    }
}
