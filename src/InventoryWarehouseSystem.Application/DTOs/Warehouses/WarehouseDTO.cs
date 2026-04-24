namespace InventoryWarehouseSystem.Application.DTOs.Warehouses;

public sealed record WarehouseDTO(int Id, string Name, string? Location, string Status);

