namespace Tests.ServicesTests;

using Infrastructure.Models;
using Moq;
using Services.OrderService;
using Services.ProductService;
using Tests.Common;

public class OrderServiceTests : SharedBaseFixture
{
    #region Fields

    private readonly Mock<IProductService> _mockProductService;
    private readonly OrderService _orderService;

    #endregion

    #region Constructor

    public OrderServiceTests()
    {
        _mockProductService = new Mock<IProductService>();
        _orderService = new OrderService(AppDbContext, _mockProductService.Object, DateTimeProvider);
    }

    #endregion

    #region Setup Methods

    private void SetupMockProductService_ReturnsProduct(int productId, string name, decimal price)
    {
        _mockProductService
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync(new Product
            {
                Id = productId,
                Name = name,
                Price = price,
                CreatedAt = DateTimeProvider.Now()
            });
    }

    private void SetupMockProductService_ReturnsNull(int productId)
    {
        _mockProductService
            .Setup(x => x.GetProductByIdAsync(productId))
            .ReturnsAsync((Product)null);
    }

    private void SetupMockProductService_ReturnsFakeProduct(Product fakeProduct)
    {
        _mockProductService
            .Setup(x => x.GetProductByIdAsync(fakeProduct.Id))
            .ReturnsAsync(fakeProduct);
    }

    private void SetupMockProductService_ReturnsMultipleProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            _mockProductService
                .Setup(x => x.GetProductByIdAsync(product.Id))
                .ReturnsAsync(product);
        }
    }

    #endregion

    #region Create Order Feature

    [Theory]
    [InlineData(1, 1, 100, 100)]
    [InlineData(2, 5, 50, 250)]
    [InlineData(3, 10, 25.50, 255)]
    [InlineData(4, 3, 333.33, 999.99)]
    public async Task CreateOrder_WithDifferentQuantities_ShouldCalculateCorrectTotal(
        int productId, int quantity, decimal price, decimal expectedTotal)
    {
        // Arrange
        SetupMockProductService_ReturnsProduct(productId, $"Product-{productId}", price);

        // Act
        var order = await _orderService.CreateOrderAsync(productId, quantity);

        // Assert
        Assert.Equal(expectedTotal, order.TotalPrice);
        Assert.Equal(quantity, order.Quantity);

        // Verify that the dependent method is called only once, as expected.
        _mockProductService.Verify(x => x.GetProductByIdAsync(productId), Times.Once);
    }

    [Fact]
    public async Task CreateOrder_WithBogusProduct_ShouldCreateOrder()
    {
        // Arrange
        var fakeProduct = new Product
        {
            Id = BaseFaker.Random.Int(1, 1000),
            Name = BaseFaker.Commerce.ProductName(),
            Price = BaseFaker.Finance.Amount(10, 5000),
            CreatedAt = DateTimeProvider.Now()
        };

        SetupMockProductService_ReturnsFakeProduct(fakeProduct);

        // Act
        var order = await _orderService.CreateOrderAsync(fakeProduct.Id, 2);

        // Assert
        Assert.NotNull(order);
        Assert.Equal(fakeProduct.Id, order.ProductId);
        Assert.Equal(fakeProduct.Name, order.ProductName);
        Assert.Equal(fakeProduct.Price * 2, order.TotalPrice);
        _mockProductService.Verify(x => x.GetProductByIdAsync(fakeProduct.Id), Times.Once);
    }

    [Fact]
    public async Task CreateMultipleOrders_WithBogusData_ShouldCreateAll()
    {
        // Arrange
        var fakeProducts = new List<Product>();
        for (int i = 0; i < 5; i++)
        {
            fakeProducts.Add(new Product
            {
                Id = i + 1,
                Name = BaseFaker.Commerce.ProductName(),
                Price = BaseFaker.Finance.Amount(10, 1000),
                CreatedAt = DateTimeProvider.Now()
            });
        }

        SetupMockProductService_ReturnsMultipleProducts(fakeProducts);

        // Act
        var orders = new List<Order>();
        foreach (var product in fakeProducts)
        {
            var order = await _orderService.CreateOrderAsync(product.Id, 1);
            orders.Add(order);
        }

        // Assert
        Assert.Equal(5, orders.Count);
        foreach (var order in orders)
        {
            Assert.True(order.Id > 0);
            Assert.NotNull(order.ProductName);
        }
    }

    [Theory]
    [InlineData(999)]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateOrder_WithInvalidProductId_ShouldThrowException(int invalidProductId)
    {
        // Arrange
        SetupMockProductService_ReturnsNull(invalidProductId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(
            () => _orderService.CreateOrderAsync(invalidProductId, 1));

        Assert.Contains("not found", exception.Message);
        _mockProductService.Verify(x => x.GetProductByIdAsync(invalidProductId), Times.Once);
    }

    [Theory]
    [InlineData(0)]      // Zero quantity
    [InlineData(1)]      // Single item
    [InlineData(100)]    // Large quantity
    [InlineData(1000)]   // Very large quantity
    public async Task CreateOrder_WithEdgeCaseQuantities_ShouldHandle(int quantity)
    {
        // Arrange
        var fakeProduct = new Product
        {
            Id = 1,
            Name = BaseFaker.Commerce.ProductName(),
            Price = 50m,
            CreatedAt = DateTimeProvider.Now()
        };

        SetupMockProductService_ReturnsFakeProduct(fakeProduct);

        // Act
        var order = await _orderService.CreateOrderAsync(1, quantity);

        // Assert
        Assert.Equal(quantity, order.Quantity);
        Assert.Equal(fakeProduct.Price * quantity, order.TotalPrice);
    }

    #endregion

    #region Get Orders Feature

    [Fact]
    public async Task GetAllOrders_ShouldNotCallProductService()
    {
        // Act
        await _orderService.GetAllOrdersAsync();

        // Assert
        // Verify that the dependent method is never called, as expected.
        _mockProductService.Verify(x => x.GetProductByIdAsync(It.IsAny<int>()), Times.Never);
    }

    #endregion

    #region Mock Verification

    [Fact]
    public async Task CreateOrder_ShouldCallProductServiceOnce()
    {
        // Arrange
        var fakeProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            Price = 50m,
            CreatedAt = DateTimeProvider.Now()
        };

        SetupMockProductService_ReturnsFakeProduct(fakeProduct);

        // Act
        await _orderService.CreateOrderAsync(1, 3);

        // Assert
        _mockProductService.Verify(x => x.GetProductByIdAsync(1), Times.Once);
    }

    #endregion
}
