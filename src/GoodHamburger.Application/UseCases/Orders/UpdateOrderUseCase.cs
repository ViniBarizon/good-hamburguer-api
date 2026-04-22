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
        var existing = await orderRepo.GetByIdAsync(id);

        if (existing is null)
            return Result<OrderDto>.Failure("Order not found.");

        var updated = new Order();

        foreach (var menuItemId in request.MenuItemIds)
        {
            var menuItem = await menuRepo.GetByIdAsync(menuItemId);

            if (menuItem is null)
                return Result<OrderDto>.Failure($"Menu item with id {menuItemId} not found.");

            var result = updated.AddItem(menuItem);

            if (result.IsFailure)
                return Result<OrderDto>.Failure(result.Error!);
        }

        await orderRepo.UpdateAsync(updated);
        return Result<OrderDto>.Success(updated.ToDto());
    }
}