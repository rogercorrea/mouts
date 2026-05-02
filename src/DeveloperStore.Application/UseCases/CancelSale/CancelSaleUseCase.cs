using DeveloperStore.Application.Interfaces;

namespace DeveloperStore.Application.UseCases.CancelSale;

public class CancelSaleUseCase
{
    private readonly ISaleRepository _repository;

    public CancelSaleUseCase(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CancelSaleRequest request)
    {
        var sale = await _repository.GetByIdAsync(request.SaleId);

        if (sale is null)
            throw new Exception("Sale not found");

        sale.Cancel();

        await _repository.UpdateAsync(sale);
    }
}