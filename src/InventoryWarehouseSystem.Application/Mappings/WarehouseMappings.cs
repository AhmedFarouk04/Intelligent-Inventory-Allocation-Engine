using InventoryWarehouseSystem.Application.DTOs.Warehouses;
using InventoryWarehouseSystem.Domain.Entities;

namespace InventoryWarehouseSystem.Application.Mappings;

public static class WarehouseMappings
{
    public static WarehouseDTO ToDto(this Warehouse warehouse)
        => new(warehouse.Id, warehouse.Name, warehouse.Location, warehouse.Status.ToString());
}

