namespace DeveloperStore.Domain.Entities;
public class Sale
{
    public Guid Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Customer { get; private set; }
    public string Branch { get; private set; }
    public bool IsCancelled { get; private set; }

    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items;

    public decimal TotalAmount => _items.Sum(i => i.Total);

    public Sale(string customer, string branch)
    {
        Id = Guid.NewGuid();
        Date = DateTime.UtcNow;
        Customer = customer;
        Branch = branch;
    }

    public void AddItem(string productId, int quantity, decimal unitPrice)
    {
        var item = new SaleItem(productId, quantity, unitPrice);
        _items.Add(item);
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        IsCancelled = true;
    }

    public void Update(string customer, string branch)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot update a cancelled sale");

        if (string.IsNullOrWhiteSpace(customer))
            throw new ArgumentException("Customer is required");

        if (string.IsNullOrWhiteSpace(branch))
            throw new ArgumentException("Branch is required");

        Customer = customer;
        Branch = branch;
    }
}