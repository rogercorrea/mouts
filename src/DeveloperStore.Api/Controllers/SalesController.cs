using DeveloperStore.Application.UseCases.CreateSale;
using DeveloperStore.Application.UseCases.CancelSale;
using DeveloperStore.Application.UseCases.GetSale;
using DeveloperStore.Domain.Exceptions;
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

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid id,
        [FromServices] CancelSaleUseCase useCase)
    {
        await useCase.ExecuteAsync(new CancelSaleRequest
        {
            SaleId = id
        });

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(
        Guid id,
        [FromServices] GetSaleUseCase useCase)
    {
        try
        {
            var result = await useCase.ExecuteAsync(id);
            return Ok(result);
        }
        catch (SaleNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] GetAllSalesUseCase useCase)
    {
        var result = await useCase.ExecuteAsync();
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateSaleRequest request,
        [FromServices] UpdateSaleUseCase useCase)
    {
        request.SaleId = id;

        await useCase.ExecuteAsync(request);

        return NoContent();
    }
}