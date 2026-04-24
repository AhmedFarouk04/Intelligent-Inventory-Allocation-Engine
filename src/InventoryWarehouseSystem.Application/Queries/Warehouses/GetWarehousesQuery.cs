using InventoryWarehouseSystem.Application.DTOs.Warehouses;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Warehouses;

public sealed record GetWarehousesQuery() : IRequest<IReadOnlyCollection<WarehouseDTO>>;

