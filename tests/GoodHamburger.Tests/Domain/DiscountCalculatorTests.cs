using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Services;

namespace GoodHamburger.Tests.Domain;

public class DiscountCalculatorTests
{
    [Fact]
    public void Sandwich_Side_Drink_Should_Apply_20_Percent_Discount()
    {
        var items = new List<MenuItem>
        {
            new(1, "CheeseBurger",  5.00m, ProductType.Sandwich),
            new(4, "French fries",  2.00m, ProductType.Side),
            new(6, "Lemon soda",    2.50m, ProductType.Drink)
        };

        var result = DiscountCalculator.Calculate(items);

        Assert.Equal(0.20m, result);
    }

    [Fact]
    public void Sandwich_Drink_Should_Apply_15_Percent_Discount()
    {
        var items = new List<MenuItem>
        {
            new(3, "CheeseBacon",   7.00m, ProductType.Sandwich),
            new(7, "Coke",          2.75m, ProductType.Drink)
        };

        Assert.Equal(0.15m, DiscountCalculator.Calculate(items));
    }

    [Fact]
    public void Sandwich_Side_Should_Apply_10_Percent_Discount()
    {
        var items = new List<MenuItem>
        {
            new(2, "CheeseEgg",                 4.50m, ProductType.Sandwich),
            new(5, "French fries with bacon",   3.50m, ProductType.Side)
        };

        Assert.Equal(0.10m, DiscountCalculator.Calculate(items));
    }

    [Fact]
    public void Without_Sandwich_Should_Not_Apply_Discount()
    {
        var items = new List<MenuItem>
        {
            new(4, "French fries",  2.00m, ProductType.Side),
            new(7, "Coke",          2.75m, ProductType.Drink)
        };

        Assert.Equal(0.00m, DiscountCalculator.Calculate(items));
    }

    [Fact]
    public void Sandwich_Only_Should_Not_Apply_Discount()
    {
        var items = new List<MenuItem>
        {
            new(1, "CheeseBurger", 5.00m, ProductType.Sandwich)
        };

        Assert.Equal(0.00m, DiscountCalculator.Calculate(items));
    }

    [Fact]
    public void Sandwich_Side_Drink_Should_Calculate_Correct_Total_After_Discount()
    {
        var items = new List<MenuItem>
        {
            new(1, "CheeseBurger",  5.00m, ProductType.Sandwich),
            new(5, "French fries with bacon", 3.50m, ProductType.Side),
            new(7, "Coke",          2.75m, ProductType.Drink)
        };

        // Subtotal = 11.25, desconto 20% = 2.25, total = 9.00
        var subtotal = items.Sum(i => i.Price);
        var discount = DiscountCalculator.Calculate(items);
        var total = subtotal - (subtotal * discount);

        Assert.Equal(11.25m, subtotal);
        Assert.Equal(0.20m, discount);
        Assert.Equal(9.00m, total);
    }
}