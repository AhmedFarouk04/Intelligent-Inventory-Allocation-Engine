using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.ValueObjects;

public class Quantity : ValueObject
{
    public int Value { get; }

    private Quantity(int value)
    {
        Value = value;
    }

    public static Quantity Create(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Quantity cannot be negative");
        }

        return new Quantity(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
