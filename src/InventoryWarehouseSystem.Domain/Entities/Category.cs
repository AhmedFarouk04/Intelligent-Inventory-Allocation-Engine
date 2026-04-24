using InventoryWarehouseSystem.SharedKernel.Base;

namespace InventoryWarehouseSystem.Domain.Entities;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();

    protected Category()
    {
    }

    public Category(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required.");
        }

        Name = name.Trim();
    }
}
