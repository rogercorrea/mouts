namespace DeveloperStore.Application.UseCases.CreateSale;

public class CreateSaleResponse
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }
}