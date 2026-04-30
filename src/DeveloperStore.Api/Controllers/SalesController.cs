using DeveloperStore.Application.UseCases.CreateSale;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSaleRequest request,
        [FromServices] CreateSaleUseCase useCase)
    {
        var result = await useCase.ExecuteAsync(request);
        return Ok(result);
    }
}