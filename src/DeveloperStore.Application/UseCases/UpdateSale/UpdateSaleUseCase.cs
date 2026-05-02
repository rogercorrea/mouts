using DeveloperStore.Application.Interfaces;
public class UpdateSaleUseCase
{
    private readonly ISaleRepository _repository;

    public UpdateSaleUseCase(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(UpdateSaleRequest request)
    {
        var sale = await _repository.GetByIdAsync(request.SaleId);

        if (sale is null)
            throw new Exception("Sale not found");

        sale.Update(request.Customer, request.Branch);

        await _repository.UpdateAsync(sale);
    }
}