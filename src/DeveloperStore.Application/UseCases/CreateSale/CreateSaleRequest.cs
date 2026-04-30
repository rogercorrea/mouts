namespace DeveloperStore.Application.UseCases.CreateSale;

public class CreateSaleRequest
{
    public string Customer { get; set; } = default!;
    public string Branch { get; set; } = default!;
    public List<CreateSaleItemRequest> Items { get; set; } = new();
}

public class CreateSaleItemRequest
{
    public string ProductId { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}