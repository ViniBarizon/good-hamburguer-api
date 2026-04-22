using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mappings;
using GoodHamburger.Domain.Common;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.UseCases.Orders;

public class GetOrderUseCase(IOrderRepository repo)
{
    public async Task<Result<OrderDto>> ExecuteAsync(Guid id)
    {
        var order = await repo.GetByIdAsync(id);

        return order is null
            ? Result<OrderDto>.Failure("Order not found.")
            : Result<OrderDto>.Success(order.ToDto());
    }
}