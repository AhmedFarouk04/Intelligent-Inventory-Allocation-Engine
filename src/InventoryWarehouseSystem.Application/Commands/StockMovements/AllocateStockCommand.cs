using InventoryWarehouseSystem.Application.DTOs.Responses;
using MediatR;

namespace InventoryWarehouseSystem.Application.Commands.StockMovements;

public record AllocateStockCommand(
    int ProductId,
    int WarehouseId,
    int Quantity,
    string? ReferenceNumber,
    string? Notes,
    string? MovedBy) : IRequest<ApiResponse<bool>>;
