using GoodHamburger.Application.DTOs;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Mappings;

public static class OrderMappings
{
    public static OrderDto ToDto(this Order order) => new(
        order.Id,
        order.CreatedAt,
        order.Items.Select(i => new MenuItemDto(i.Id, i.Name, i.Price, i.Type.ToString())),
        order.Subtotal,
        order.DiscountPercentage,
        order.DiscountAmount,
        order.Total
    );
}