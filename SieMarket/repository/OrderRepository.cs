using SieMarket.model;

namespace SieMarket.repository;

public class OrderRepository
{
    private static OrderRepository _instance;
    private List<Order> _orders;

    private OrderRepository()
    {
        _orders = new List<Order>();
    }

    public static OrderRepository GetInstance()
    {
        if (_instance == null)
        {
            _instance = new OrderRepository();
        }

        return _instance;
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public Order GetOrderById(int id)
    {
        foreach (var order in _orders)
        {
            if (order.Id == id)
                return order;
        }

        throw new ArgumentException("Order not found.");
    }

    public List<Order> GetAllOrders()
    {
        return _orders;
    }
}