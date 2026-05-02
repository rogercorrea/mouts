public class UpdateSaleRequest
{
    public Guid SaleId { get; set; }
    public string Customer { get; set; } = default!;
    public string Branch { get; set; } = default!;
}