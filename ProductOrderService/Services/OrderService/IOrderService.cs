namespace Services.OrderService;

using Infrastructure.Models;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int productId, int quantity);

    Task<IEnumerable<Order>> GetAllOrdersAsync();
}
