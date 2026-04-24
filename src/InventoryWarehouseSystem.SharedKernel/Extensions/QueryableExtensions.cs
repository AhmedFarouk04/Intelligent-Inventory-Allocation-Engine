using InventoryWarehouseSystem.SharedKernel.Results;

namespace InventoryWarehouseSystem.SharedKernel.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = query.Count();
        var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return await Task.FromResult(new PagedResult<T>(items, page, pageSize, totalCount));
    }
}
