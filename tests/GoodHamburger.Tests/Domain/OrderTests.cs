using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Add_Valid_Item_Should_Succeed()
    {
        var order = new Order();
        var item = new MenuItem(1, "CheeseBurger", 5.00m, ProductType.Sandwich);

        var result = order.AddItem(item);

        Assert.True(result.IsSuccess);
        Assert.Single(order.Items);
    }

    [Fact]
    public void Add_Duplicate_Sandwich_Should_Fail()
    {
        var order = new Order();
        order.AddItem(new MenuItem(1, "CheeseBurger", 5.00m, ProductType.Sandwich));

        var result = order.AddItem(new MenuItem(2, "CheeseEgg", 4.50m, ProductType.Sandwich));

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Add_Duplicate_Side_Should_Fail()
    {
        var order = new Order();
        order.AddItem(new MenuItem(4, "French fries", 2.00m, ProductType.Side));

        var result = order.AddItem(new MenuItem(5, "French fries with bacon", 3.50m, ProductType.Side));

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Add_Duplicate_Drink_Should_Fail()
    {
        var order = new Order();
        order.AddItem(new MenuItem(6, "Lemon soda", 2.50m, ProductType.Drink));

        var result = order.AddItem(new MenuItem(7, "Coke", 2.75m, ProductType.Drink));

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Total_Should_Apply_20_Percent_Discount_Correctly()
    {
        var order = new Order();
        order.AddItem(new MenuItem(1, "CheeseBurger",  5.00m, ProductType.Sandwich));
        order.AddItem(new MenuItem(4, "French fries",  2.00m, ProductType.Side));
        order.AddItem(new MenuItem(6, "Lemon soda",    2.50m, ProductType.Drink));

        // Subtotal = 9.50, desconto 20% = 1.90, total = 7.60
        Assert.Equal(9.50m,  order.Subtotal);
        Assert.Equal(0.20m,  order.DiscountPercentage);
        Assert.Equal(1.90m,  order.DiscountAmount);
        Assert.Equal(7.60m,  order.Total);
    }

    [Fact]
    public void Total_Should_Apply_15_Percent_Discount_Correctly()
    {
        var order = new Order();
        order.AddItem(new MenuItem(3, "CheeseBacon",   7.00m, ProductType.Sandwich));
        order.AddItem(new MenuItem(7, "Coke",          2.75m, ProductType.Drink));

        // Subtotal = 9.75, desconto 15% = 1.4625, total = 8.2875
        Assert.Equal(9.75m,   order.Subtotal);
        Assert.Equal(0.15m,   order.DiscountPercentage);
        Assert.Equal(8.2875m, order.Total);
    }

    [Fact]
    public void Remove_Nonexistent_Item_Should_Fail()
    {
        var order = new Order();

        var result = order.RemoveItem(99);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Remove_Existing_Item_Should_Succeed()
    {
        var order = new Order();
        order.AddItem(new MenuItem(1, "CheeseBurger", 5.00m, ProductType.Sandwich));

        var result = order.RemoveItem(1);

        Assert.True(result.IsSuccess);
        Assert.Empty(order.Items);
    }
}