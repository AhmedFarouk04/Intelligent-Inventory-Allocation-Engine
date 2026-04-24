namespace InventoryWarehouseSystem.Domain.Repositories;

public interface IUnitOfWork : SharedKernel.Interfaces.IUnitOfWork, IDisposable
{
    IProductRepository Products { get; }
    IWarehouseRepository Warehouses { get; }
    IStockRepository Stocks { get; }
    IStockMovementRepository StockMovements { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
