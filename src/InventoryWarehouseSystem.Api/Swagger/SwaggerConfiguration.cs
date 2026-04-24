using Microsoft.OpenApi.Models;

namespace InventoryWarehouseSystem.Api.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Inventory Warehouse System API",
                Version = "v1",
                Description = "Intelligent Inventory Allocation Engine"
            });
        });

        return services;
    }
}
