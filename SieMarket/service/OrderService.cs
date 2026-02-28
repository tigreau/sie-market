using SieMarket.model;
using SieMarket.repository;

namespace SieMarket.service;

public class OrderService
{
    private static OrderService _instance;
    private OrderRepository _repository;

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
}