using InventoryWarehouseSystem.Domain.Enums;

namespace InventoryWarehouseSystem.Domain.ValueObjects;

public readonly record struct MovementType(MovementTypeEnum Value)
{
    public static MovementType In => new(MovementTypeEnum.In);
    public static MovementType Out => new(MovementTypeEnum.Out);
    public static MovementType Transfer => new(MovementTypeEnum.Transfer);
    public static MovementType Adjustment => new(MovementTypeEnum.Adjustment);
}
