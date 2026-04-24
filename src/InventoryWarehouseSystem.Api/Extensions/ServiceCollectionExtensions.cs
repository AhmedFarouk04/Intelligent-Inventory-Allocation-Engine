using InventoryWarehouseSystem.Application;
using InventoryWarehouseSystem.Infrastructure;

namespace InventoryWarehouseSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
