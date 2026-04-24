# Inventory Warehouse System

Intelligent Inventory Allocation Engine built with .NET 8, Clean Architecture, CQRS, EF Core, and automated tests.

## Run
```powershell
dotnet restore
dotnet build InventoryWarehouseSystem.sln
dotnet test InventoryWarehouseSystem.sln --no-build
dotnet run --project .\src\InventoryWarehouseSystem.Api --urls http://localhost:5088
```

Swagger: `http://localhost:5088/swagger`

## Docs
- `docs/ARCHITECTURE.md`
- `docs/DECISIONS.md`
- `docs/SETUP.md`
- `docs/TESTING.md`
