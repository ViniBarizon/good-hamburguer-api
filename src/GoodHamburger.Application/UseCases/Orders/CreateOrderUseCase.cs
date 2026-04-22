using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mappings;
using GoodHamburger.Domain.Common;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.UseCases.Orders;

public class CreateOrderUseCase(IOrderRepository orderRepo, IMenuItemRepository menuRepo)
{
    public async Task<Result<OrderDto>> ExecuteAsync(CreateOrderRequest request)
    {
        var order = new Domain.Entities.Order();

        foreach (var id in request.MenuItemIds)
        {
            var menuItem = await menuRepo.GetByIdAsync(id);

            if (menuItem is null)
                return Result<OrderDto>.Failure($"Menu item with id {id} not found.");

            var result = order.AddItem(menuItem);

            if (result.IsFailure)
                return Result<OrderDto>.Failure(result.Error!);
        }

        await orderRepo.AddAsync(order);
        return Result<OrderDto>.Success(order.ToDto());
    }
}