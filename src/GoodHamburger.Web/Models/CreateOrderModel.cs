namespace GoodHamburger.Web.Models;

public class CreateOrderModel
{
    public List<int> MenuItemIds { get; set; } = new();
}