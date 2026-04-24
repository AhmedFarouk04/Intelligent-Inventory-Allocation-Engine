using InventoryWarehouseSystem.Domain.Entities;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.StockMovements;

public record GetMovementHistoryQuery(int? ProductId, int? WarehouseId, DateTime? From, DateTime? To)
    : IRequest<IReadOnlyCollection<StockMovement>>;
