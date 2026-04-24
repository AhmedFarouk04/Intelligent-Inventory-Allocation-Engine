namespace InventoryWarehouseSystem.SharedKernel.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? items) => items is null || !items.Any();
}
