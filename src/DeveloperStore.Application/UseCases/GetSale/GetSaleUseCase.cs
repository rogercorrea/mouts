using DeveloperStore.Application.Interfaces;
using DeveloperStore.Domain.Exceptions;

namespace DeveloperStore.Application.UseCases.GetSale;

public class GetSaleUseCase
{
    private readonly ISaleRepository _repository;

    public GetSaleUseCase(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetSaleResponse?> ExecuteAsync(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);

        if (sale is null)
            throw new SaleNotFoundException(id);

        return new GetSaleResponse
        {
            Id = sale.Id,
            Customer = sale.Customer,
            Branch = sale.Branch,
            IsCancelled = sale.IsCancelled,
            TotalAmount = sale.TotalAmount,
            Items = sale.Items.Select(i => new GetSaleItemResponse
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                Total = i.Total
            }).ToList()
        };
    }
}