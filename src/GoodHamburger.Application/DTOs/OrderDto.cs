namespace GoodHamburger.Application.DTOs;

public record OrderDto(
    Guid Id,
    DateTime CreatedAt,
    IEnumerable<MenuItemDto> Items,
    decimal Subtotal,
    decimal DiscountPercentage,
    decimal DiscountAmount,
    decimal Total
);