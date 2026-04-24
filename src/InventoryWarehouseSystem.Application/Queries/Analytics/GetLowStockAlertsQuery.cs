using InventoryWarehouseSystem.Application.DTOs.Products;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Analytics;

public record GetLowStockAlertsQuery() : IRequest<IReadOnlyCollection<ProductDTO>>;
