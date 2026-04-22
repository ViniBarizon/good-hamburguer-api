using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mappings;
using GoodHamburger.Domain.Common;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.UseCases.Orders;

public class UpdateOrderUseCase(IOrderRepository orderRepo, IMenuItemRepository menuRepo)
{
    public async Task<Result<OrderDto>> ExecuteAsync(Guid id, UpdateOrderRequest request)
    {
        var order = await orderRepo.GetByIdAsync(id);

        if (order is null)
            return Result<OrderDto>.Failure("Order not found.");

        order.ClearItems();

        foreach (var menuItemId in request.MenuItemIds)
        {
            var menuItem = await menuRepo.GetByIdAsync(menuItemId);

            if (menuItem is null)
                return Result<OrderDto>.Failure($"Menu item with id {menuItemId} not found.");

            var result = order.AddItem(menuItem);

            if (result.IsFailure)
                return Result<OrderDto>.Failure(result.Error!);
        }

        await orderRepo.UpdateAsync(order);
        return Result<OrderDto>.Success(order.ToDto());
    }
}