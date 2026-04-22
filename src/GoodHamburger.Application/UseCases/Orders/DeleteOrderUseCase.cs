using GoodHamburger.Domain.Common;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.UseCases.Orders;

public class DeleteOrderUseCase(IOrderRepository repo)
{
    public async Task<Result> ExecuteAsync(Guid id)
    {
        var order = await repo.GetByIdAsync(id);

        if (order is null)
            return Result.Failure("Order not found.");

        await repo.DeleteAsync(id);
        return Result.Success();
    }
}