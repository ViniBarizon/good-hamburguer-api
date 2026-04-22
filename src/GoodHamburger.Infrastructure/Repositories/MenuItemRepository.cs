using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private static readonly List<MenuItem> _menu = new()
    {
        new MenuItem(1, "CheeseBurger",               5.00m, ProductType.Sandwich),
        new MenuItem(2, "CheeseEgg",                  4.50m, ProductType.Sandwich),
        new MenuItem(3, "CheeseBacon",                7.00m, ProductType.Sandwich),
        new MenuItem(4, "French fries",               2.00m, ProductType.Side),
        new MenuItem(5, "French fries with bacon",    3.50m, ProductType.Side),
        new MenuItem(6, "Lemon soda",                 2.50m, ProductType.Drink),
        new MenuItem(7, "Coke",                       2.75m, ProductType.Drink),
    };

    public Task<MenuItem?> GetByIdAsync(int id) =>
        Task.FromResult(_menu.FirstOrDefault(i => i.Id == id));

    public Task<IEnumerable<MenuItem>> GetAllAsync() =>
        Task.FromResult<IEnumerable<MenuItem>>(_menu);
}