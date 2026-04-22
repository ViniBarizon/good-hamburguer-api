namespace GoodHamburger.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private readonly List<MenuItem> _items = new();
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public decimal Subtotal => _items.Sum(i => i.Price);
    public decimal DiscountPercentage => DiscountCalculator.Calculate(_items);
    public decimal DiscountAmount => Subtotal * DiscountPercentage;
    public decimal Total => Subtotal - DiscountAmount;

    public Result AddItem(MenuItem item)
    {
        if (_items.Any(i => i.Type == item.Type))
            return Result.Failure($"Order already contains a {item.Type}.");

        _items.Add(item);
        return Result.Success();
    }

    public Result RemoveItem(int menuItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == menuItemId);
        if (item is null) return Result.Failure("Item not found in order.");
        _items.Remove(item);
        return Result.Success();
    }
}