# Setup

1. Ensure .NET SDK 8 installed.
2. Update connection string in `src/InventoryWarehouseSystem.Api/appsettings.json`.
3. Run:
```bash
dotnet restore
dotnet build
dotnet test
```
4. Apply DB migration:
```bash
dotnet ef database update --project .\src\InventoryWarehouseSystem.Infrastructure --startup-project .\src\InventoryWarehouseSystem.Api
```
5. Start API:
```bash
dotnet run --project .\src\InventoryWarehouseSystem.Api --urls http://localhost:5088
```
6. Open Swagger: `http://localhost:5088/swagger`
