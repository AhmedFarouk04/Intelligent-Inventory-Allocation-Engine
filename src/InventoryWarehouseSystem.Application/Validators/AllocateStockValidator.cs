using FluentValidation;
using InventoryWarehouseSystem.Application.DTOs.StockMovements;

namespace InventoryWarehouseSystem.Application.Validators;

public class AllocateStockValidator : AbstractValidator<AllocateStockRequestDTO>
{
    public AllocateStockValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.WarehouseId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
