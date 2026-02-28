namespace SieMarket.model;

public class Order
{
    // Auto incremented ID like in a real database
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string CustomerName { get; private set; }
    public List<OrderItem> Items { get; private set; }

    public Order(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be empty.");

        Id = _nextId++;
        CustomerName = customerName;
        Items = new List<OrderItem>();
    }
}