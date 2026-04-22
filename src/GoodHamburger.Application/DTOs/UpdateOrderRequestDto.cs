
namespace GoodHamburger.Application.DTOs;

public record UpdateOrderRequest(
    IEnumerable<int> MenuItemIds
);