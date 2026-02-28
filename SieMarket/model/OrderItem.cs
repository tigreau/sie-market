namespace SieMarket.model;

public class OrderItem
{
    // Auto incremented ID like in a real database
    private static int _nextId = 1;

    public int Id { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public OrderItem(string productName, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be empty.");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.");

        Id = _nextId++;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}