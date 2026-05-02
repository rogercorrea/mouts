using DeveloperStore.Application.Interfaces;
using DeveloperStore.Application.UseCases.GetSale;
public class GetAllSalesUseCase
{
    private readonly ISaleRepository _repository;

    public GetAllSalesUseCase(ISaleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetSaleResponse>> ExecuteAsync()
    {
        var sales = await _repository.GetAllAsync();

        return sales.Select(s => new GetSaleResponse
        {
            Id = s.Id,
            Customer = s.Customer,
            Branch = s.Branch,
            IsCancelled = s.IsCancelled,
            TotalAmount = s.TotalAmount
        }).ToList();
    }
}