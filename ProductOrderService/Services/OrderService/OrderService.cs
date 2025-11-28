namespace Services.OrderService;

using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Services.DateTimeProvider;
using Services.ProductService;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IProductService _productService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(
        AppDbContext context,
        IProductService productService,
        IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _productService = productService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Order> CreateOrderAsync(int productId, int quantity)
    {
        var product = await _productService.GetProductByIdAsync(productId);

        if (product == null)
        {
            throw new Exception($"Product with ID {productId} not found!");
        }

        var order = new Order
        {
            ProductId = productId,
            Quantity = quantity,
            ProductName = product.Name,
            TotalPrice = product.Price * quantity,
            CreatedAt = _dateTimeProvider.Now()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }
}
