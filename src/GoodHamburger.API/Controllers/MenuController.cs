using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MenuController(IMenuItemRepository repo) : ControllerBase
{
    /// <summary>
    /// Returns all available menu items.
    /// </summary>
    /// <response code="200">Menu returned successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MenuItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenu() =>
        Ok(await repo.GetAllAsync());
}