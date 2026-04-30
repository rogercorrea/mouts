using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Application.UseCases.CreateSale;

public class CreateSaleUseCase
{
    private readonly ISaleRepository _repository;

    public CreateSaleUseCase(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateSaleResponse> ExecuteAsync(CreateSaleRequest request)
    {
        var sale = new Sale(request.Customer, request.Branch);

        foreach (var item in request.Items)
        {
            sale.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
        }

        await _repository.AddAsync(sale);

        return new CreateSaleResponse
        {
            SaleId = sale.Id,
            TotalAmount = sale.TotalAmount
        };
    }
}