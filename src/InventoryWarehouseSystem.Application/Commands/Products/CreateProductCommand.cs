using InventoryWarehouseSystem.Application.DTOs.Products;
using InventoryWarehouseSystem.Application.DTOs.Responses;
using MediatR;

namespace InventoryWarehouseSystem.Application.Commands.Products;

public record CreateProductCommand(string Sku, string Name, string? Description, int CategoryId, int ReorderLevel)
    : IRequest<ApiResponse<ProductDTO>>;
