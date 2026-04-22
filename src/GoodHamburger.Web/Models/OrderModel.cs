namespace GoodHamburger.Web.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<MenuItemModel> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Total { get; set; }
}