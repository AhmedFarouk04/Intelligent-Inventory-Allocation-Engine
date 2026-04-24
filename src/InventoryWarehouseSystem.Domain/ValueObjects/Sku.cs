using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.ValueObjects;

public class Sku : ValueObject
{
    public string Value { get; }

    private Sku(string value)
    {
        Value = value;
    }

    public static Sku Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("SKU cannot be empty");
        }

        if (value.Length > 50)
        {
            throw new ArgumentException("SKU cannot exceed 50 characters");
        }

        return new Sku(value.ToUpperInvariant());
    }

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
