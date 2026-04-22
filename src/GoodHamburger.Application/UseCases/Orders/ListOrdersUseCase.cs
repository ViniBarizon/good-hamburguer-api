using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mappings;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.UseCases.Orders;

public class ListOrdersUseCase(IOrderRepository repo)
{
    public async Task<IEnumerable<OrderDto>> ExecuteAsync() =>
        (await repo.GetAllAsync()).Select(o => o.ToDto());
}