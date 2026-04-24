using InventoryWarehouseSystem.Domain.Enums;
using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.Entities;

public class Warehouse : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string? Location { get; private set; }
    public int Capacity { get; private set; }
    public WarehouseStatus Status { get; private set; } = WarehouseStatus.Active;

    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    protected Warehouse()
    {
    }

    public Warehouse(string name, int capacity, string? location = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Warehouse name is required.");
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.");
        }

        Name = name.Trim();
        Capacity = capacity;
        Location = location;
    }

    public void Deactivate() => Status = WarehouseStatus.Inactive;
}
