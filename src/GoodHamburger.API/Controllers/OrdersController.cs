using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    CreateOrderUseCase create,
    ListOrdersUseCase list,
    GetOrderUseCase get,
    UpdateOrderUseCase update,
    DeleteOrderUseCase delete) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var result = await create.ExecuteAsync(request);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value)
            : BadRequest(new { error = result.Error });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await list.ExecuteAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await get.ExecuteAsync(id);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new { error = result.Error });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrderRequest request)
    {
        var result = await update.ExecuteAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(new { error = result.Error });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await delete.ExecuteAsync(id);

        return result.IsSuccess
            ? NoContent()
            : NotFound(new { error = result.Error });
    }
}