namespace GoodHamburger.Domain.Entities;

public class MenuItem
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public ProductType Type { get; private set; }

    public MenuItem(int id, string name, decimal price, ProductType type)
    {
        Id = id; Name = name; Price = price; Type = type;
    }
}