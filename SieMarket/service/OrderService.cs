using SieMarket.model;
using SieMarket.repository;

namespace SieMarket.service;

public class OrderService
{
    private static OrderService? _instance;
    private OrderRepository _repository;
    
    private const decimal DiscountThreshold = 500m;
    private const decimal DiscountMultiplier = 0.9m;

    private OrderService()
    {
        _repository = OrderRepository.GetInstance();
    }

    public static OrderService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new OrderService();
        }

        return _instance;
    }

    public void AddOrder(Order order)
    {
        _repository.AddOrder(order);
    }
    
    // Exercise 2.2
    public decimal CalculateFinalPrice(int orderId)
    {
        Order order = _repository.GetOrderById(orderId);

        decimal total = 0;
        foreach (var item in order.Items)
        {
            total += item.Quantity * item.UnitPrice;
        }

        if (total > DiscountThreshold)
        {
            total *= DiscountMultiplier;
        }

        return Math.Round(total, 2, MidpointRounding.AwayFromZero);
    }
    
    // Exercise 2.3
    public string GetTopSpendingCustomer()
    {
        List<Order> orders = _repository.GetAllOrders();

        if (orders.Count == 0)
        {
            throw new InvalidOperationException("No orders found.");
        }

        Dictionary<string, decimal> spending = new Dictionary<string, decimal>();

        foreach (var order in orders)
        {
            decimal finalPrice = CalculateFinalPrice(order.Id);

            if (spending.ContainsKey(order.CustomerName))
                spending[order.CustomerName] += finalPrice;
            else
                spending[order.CustomerName] = finalPrice;
        }

        string topCustomer = "";
        decimal maxSpent = 0;

        foreach (var entry in spending)
        {
            if (entry.Value > maxSpent)
            {
                maxSpent = entry.Value;
                topCustomer = entry.Key;
            }
        }

        return topCustomer;
    }
}