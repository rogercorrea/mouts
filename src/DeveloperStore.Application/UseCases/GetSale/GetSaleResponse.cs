namespace DeveloperStore.Application.UseCases.GetSale;
public class GetSaleResponse
{
    public Guid Id { get; set; }
    public string Customer { get; set; } = default!;
    public string Branch { get; set; } = default!;
    public bool IsCancelled { get; set; }
    public decimal TotalAmount { get; set; }
    public List<GetSaleItemResponse> Items { get; set; } = new();
}

public class GetSaleItemResponse
{
    public string ProductId { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}