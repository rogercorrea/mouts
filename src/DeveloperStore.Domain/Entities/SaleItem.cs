namespace DeveloperStore.Domain.Entities;

public class SaleItem
{
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }

    public SaleItem(string productId, int quantity, decimal unitPrice)
    {
        if (quantity > 20)
            throw new ArgumentException("Cannot sell more than 20 identical items.");

        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;

        Discount = CalculateDiscount();
        Total = CalculateTotal();
    }

    private decimal CalculateDiscount()
    {
        if (Quantity >= 10)
            return 0.20m;

        if (Quantity >= 4)
            return 0.10m;

        return 0m;
    }

    private decimal CalculateTotal()
    {
        var subtotal = Quantity * UnitPrice;
        return subtotal - (subtotal * Discount);
    }
}