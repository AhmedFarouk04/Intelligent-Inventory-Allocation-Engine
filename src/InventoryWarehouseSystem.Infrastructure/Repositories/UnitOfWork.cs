using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using InventoryWarehouseSystem.SharedKernel.Base;
using InventoryWarehouseSystem.SharedKernel.Events;

namespace InventoryWarehouseSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public IProductRepository Products { get; }
    public IWarehouseRepository Warehouses { get; }
    public IStockRepository Stocks { get; }
    public IStockMovementRepository StockMovements { get; }

    public UnitOfWork(ApplicationDbContext context, IDomainEventDispatcher domainEventDispatcher)
    {
        _context = context;
        _domainEventDispatcher = domainEventDispatcher;
        Products = new ProductRepository(context);
        Warehouses = new WarehouseRepository(context);
        Stocks = new StockRepository(context);
        StockMovements = new StockMovementRepository(context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
        return result;
    }

    public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _context.Database.CommitTransactionAsync(cancellationToken);
            await DispatchDomainEventsAsync(cancellationToken);
        }
        catch
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        => _context.Database.RollbackTransactionAsync(cancellationToken);

    public void Dispose() => _context.Dispose();

    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var aggregates = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        if (aggregates.Length == 0)
        {
            return;
        }

        var events = aggregates.SelectMany(e => e.DomainEvents).ToArray();
        foreach (var aggregate in aggregates)
        {
            aggregate.ClearDomainEvents();
        }

        await _domainEventDispatcher.DispatchAsync(events, cancellationToken);
    }
}
