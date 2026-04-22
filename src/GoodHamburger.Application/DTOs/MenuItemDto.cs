
namespace GoodHamburger.Application.DTOs;

public record MenuItemDto(
    int Id, 
    string Name, 
    decimal Price, 
    string Type
);