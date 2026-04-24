using InventoryWarehouseSystem.Application.DTOs.Responses;
using InventoryWarehouseSystem.Application.Exceptions;
using InventoryWarehouseSystem.Domain.Entities;
using InventoryWarehouseSystem.Domain.Enums;
using InventoryWarehouseSystem.Domain.Repositories;
using MediatR;

namespace InventoryWarehouseSystem.Application.Commands.StockMovements;

public class AllocateStockCommandHandler : IRequestHandler<AllocateStockCommand, ApiResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AllocateStockCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<bool>> Handle(AllocateStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _unitOfWork.Stocks.GetByProductAndWarehouseAsync(request.ProductId, request.WarehouseId, cancellationToken);
        if (stock is null)
        {
            return ApiResponse<bool>.Failure("Stock record was not found", 404);
        }

        if (stock.CurrentQuantity < request.Quantity)
        {
            throw new InsufficientStockException(request.ProductId, request.WarehouseId, request.Quantity, stock.CurrentQuantity);
        }

        stock.Allocate(request.Quantity);

        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is not null)
        {
            stock.RaiseLowStockAlertIfNeeded(product.ReorderLevel);
        }

        var movement = new StockMovement(
            request.ProductId,
            request.WarehouseId,
            MovementTypeEnum.Out,
            request.Quantity,
            request.Notes,
            request.ReferenceNumber,
            request.MovedBy);

        await _unitOfWork.StockMovements.AddAsync(movement, cancellationToken);
        _unitOfWork.Stocks.Update(stock);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ApiResponse<bool>.SuccessResponse(true, "Stock allocated successfully");
    }
}
