using GoodHamburger.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(IMenuItemRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMenu() =>
        Ok(await repo.GetAllAsync());
}