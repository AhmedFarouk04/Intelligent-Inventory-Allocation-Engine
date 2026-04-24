using InventoryWarehouseSystem.Domain.Events;
using InventoryWarehouseSystem.Domain.Repositories;
using InventoryWarehouseSystem.Infrastructure.HealthChecks;
using InventoryWarehouseSystem.Infrastructure.Messaging;
using InventoryWarehouseSystem.Infrastructure.Messaging.EventHandlers;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using InventoryWarehouseSystem.Infrastructure.Repositories;
using InventoryWarehouseSystem.Infrastructure.Services;
using InventoryWarehouseSystem.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InventoryWarehouseSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventHandler<StockMovedEvent>, StockMovedEventHandler>();
        services.AddScoped<IDomainEventHandler<LowStockAlertEvent>, LowStockAlertEventHandler>();

        services.AddScoped<StockAllocationService>();
        services.AddScoped<LowStockAlertService>();
        services.AddScoped<AuditService>();

        services.AddHealthChecks()
            .AddCheck<ApplicationDbContextHealthCheck>("db", HealthStatus.Unhealthy, new[] { "ready" });

        return services;
    }
}

