using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Application.DTOs.Responses;
using MediatR;

namespace InventoryWarehouseSystem.Application.Queries.Products;

public record GetProductsQuery(int Page = 1, int PageSize = 10) : IRequest<ApiPagedResponse<ProductDTO>>;
