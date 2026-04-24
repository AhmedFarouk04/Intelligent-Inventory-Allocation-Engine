using InventoryWarehouseSystem.Api.Extensions;
using InventoryWarehouseSystem.Api.Swagger;
using InventoryWarehouseSystem.Infrastructure.Persistence.Data;
using InventoryWarehouseSystem.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddProjectServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (db.Database.IsSqlServer())
    {
        await db.Database.MigrateAsync();
        await InitialDataSeeder.SeedAsync(db);
    }
}

app.UseProjectPipeline();

app.Run();

public partial class Program;
