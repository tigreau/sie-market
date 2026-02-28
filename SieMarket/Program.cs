using SieMarket.model;
using SieMarket.service;

namespace SieMarket;

public class Program
{
    public static void Main(string[] args)
    {
        OrderService service = OrderService.GetInstance();

        // Order 1 - Alice (total > 500€, should get discount)
        Order order1 = new Order("Alice");
        order1.Items.Add(new OrderItem("Laptop", 1, 899.99m));
        order1.Items.Add(new OrderItem("Mouse", 2, 25.50m));
        service.AddOrder(order1);

        // Order 2 - Bob (total < 500€, no discount)
        Order order2 = new Order("Bob");
        order2.Items.Add(new OrderItem("Keyboard", 1, 75.00m));
        order2.Items.Add(new OrderItem("Headset", 3, 59.99m));
        service.AddOrder(order2);

        // Order 3 - Alice again (total > 500€, should get discount)
        Order order3 = new Order("Alice");
        order3.Items.Add(new OrderItem("Monitor", 2, 349.99m));
        order3.Items.Add(new OrderItem("USB Cable", 5, 9.99m));
        service.AddOrder(order3);
        
        Console.WriteLine("=== Final Prices ===");
        Console.WriteLine("Order 1 (Alice): " + service.CalculateFinalPrice(order1.Id) + "€");
        Console.WriteLine("Order 2 (Bob): " + service.CalculateFinalPrice(order2.Id) + "€");
        Console.WriteLine("Order 3 (Alice): " + service.CalculateFinalPrice(order3.Id) + "€");
    }
}