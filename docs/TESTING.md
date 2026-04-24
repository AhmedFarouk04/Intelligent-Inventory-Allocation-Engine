# Testing Guide

## 1) Build and Unit/Integration Tests
```powershell
dotnet restore
dotnet build InventoryWarehouseSystem.sln
dotnet test InventoryWarehouseSystem.sln --no-build
```

Expected:
- Build: `0 Error(s)`
- Tests: `14 Passed`

## 2) Database Migration
```powershell
dotnet ef database update --project .\src\InventoryWarehouseSystem.Infrastructure --startup-project .\src\InventoryWarehouseSystem.Api
```

## 3) Run API
```powershell
dotnet run --project .\src\InventoryWarehouseSystem.Api --urls http://localhost:5088
```

## 4) Verify Health
```powershell
Invoke-WebRequest http://localhost:5088/api/health | Select-Object -ExpandProperty Content
```

Expected response includes:
- `status = Healthy`

## 5) Try Endpoints
Use:
- `src/InventoryWarehouseSystem.Api/InventoryWarehouseSystem.Api.http`
- or Swagger: `http://localhost:5088/swagger`

## 6) Quick Smoke Script
```powershell
.\scripts\smoke-test.ps1
```
