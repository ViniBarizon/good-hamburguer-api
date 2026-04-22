
namespace GoodHamburger.Application.DTOs;

public record CreateOrderRequestDto(
    IEnumerable<int> MenuItemIds
);  