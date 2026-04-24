namespace InventoryWarehouseSystem.SharedKernel.Interfaces;

public interface ISpecification<T>
{
    System.Linq.Expressions.Expression<Func<T, bool>>? Criteria { get; }
    List<System.Linq.Expressions.Expression<Func<T, object>>> Includes { get; }
    System.Linq.Expressions.Expression<Func<T, object>>? OrderBy { get; }
    System.Linq.Expressions.Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
