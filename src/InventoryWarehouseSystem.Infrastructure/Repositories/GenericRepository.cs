using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using InventoryWarehouseSystem.SharedKernel.Base;
using InventoryWarehouseSystem.SharedKernel.Interfaces;
using InventoryWarehouseSystem.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;

namespace InventoryWarehouseSystem.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await DbSet.ToListAsync(cancellationToken);

    public async Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<T>> ListBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).ToListAsync(cancellationToken);

    public async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).CountAsync(cancellationToken);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => DbSet.AddAsync(entity, cancellationToken).AsTask();

    public void Update(T entity) => DbSet.Update(entity);
    public void Delete(T entity) => DbSet.Remove(entity);

    protected IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        var query = DbSet.AsQueryable();

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}
