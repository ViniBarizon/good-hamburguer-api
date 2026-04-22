using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

/// <summary>
/// Handles order management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController(
    CreateOrderUseCase create,
    ListOrdersUseCase list,
    GetOrderUseCase get,
    UpdateOrderUseCase update,
    DeleteOrderUseCase delete) : ControllerBase
{
   /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <response code="201">Order created successfully.</response>
    /// <response code="400">Invalid items or duplicate item types.</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderRequest request)
    {
        var result = await create.ExecuteAsync(request);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value)
            : BadRequest(new { error = result.Error });
    }

    /// <summary>
    /// Returns all orders.
    /// </summary>
    /// <response code="200">Orders returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll() =>
        Ok(await list.ExecuteAsync());

    /// <summary>
    /// Returns a specific order by ID.
    /// </summary>
    /// <response code="200">Order found and returned.</response>
    /// <response code="404">Order not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var result = await get.ExecuteAsync(id);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new { error = result.Error });
    }

    /// <summary>
    /// Updates an existing order, replacing all its items.
    /// </summary>
    /// <response code="200">Order updated successfully.</response>
    /// <response code="400">Invalid items or duplicate item types.</response>
    /// <response code="404">Order not found.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderRequest request)
    {
        var result = await update.ExecuteAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(new { error = result.Error });
    }


    /// <summary>
    /// Removes an order.
    /// </summary>
    /// <response code="204">Order removed successfully.</response>
    /// <response code="404">Order not found.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await delete.ExecuteAsync(id);

        return result.IsSuccess
            ? NoContent()
            : NotFound(new { error = result.Error });
    }
}