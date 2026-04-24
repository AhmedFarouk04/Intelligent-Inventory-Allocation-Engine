using InventoryWarehouseSystem.SharedKernel.Interfaces;

namespace InventoryWarehouseSystem.SharedKernel.Specifications;

public class Specification<T> : ISpecification<T>
{
    public System.Linq.Expressions.Expression<Func<T, bool>>? Criteria { get; protected set; }
    public List<System.Linq.Expressions.Expression<Func<T, object>>> Includes { get; } = new();
    public System.Linq.Expressions.Expression<Func<T, object>>? OrderBy { get; protected set; }
    public System.Linq.Expressions.Expression<Func<T, object>>? OrderByDescending { get; protected set; }
    public int Take { get; protected set; }
    public int Skip { get; protected set; }
    public bool IsPagingEnabled { get; protected set; }

    protected void AddInclude(System.Linq.Expressions.Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
    protected void AddOrderBy(System.Linq.Expressions.Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderByDescending(System.Linq.Expressions.Expression<Func<T, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
