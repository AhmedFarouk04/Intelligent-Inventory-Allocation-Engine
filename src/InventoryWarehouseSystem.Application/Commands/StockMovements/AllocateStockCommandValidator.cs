using FluentValidation;

namespace InventoryWarehouseSystem.Application.Commands.StockMovements;

public class AllocateStockCommandValidator : AbstractValidator<AllocateStockCommand>
{
    public AllocateStockCommandValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.WarehouseId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.ReferenceNumber).MaximumLength(100);
        RuleFor(x => x.Notes).MaximumLength(500);
        RuleFor(x => x.MovedBy).MaximumLength(100);
    }
}
