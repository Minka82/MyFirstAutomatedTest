namespace Tests.ServicesTests;

using Infrastructure.Models;
using Services.ProductService;
using Tests.Common;

public class ProductServiceTests : SharedBaseFixture
{
    #region Fields

    private readonly ProductService _productService;

    #endregion

    #region Constructor

    public ProductServiceTests()
    {
        _productService = new ProductService(AppDbContext, DateTimeProvider);
    }

    #endregion

    #region Create Product Feature

    [Theory]
    [InlineData("Gaming Laptop", 2500.00)]
    [InlineData("Wireless Mouse", 45.50)]
    [InlineData("Mechanical Keyboard", 120.99)]
    [InlineData("4K Monitor", 450.00)]
    public async Task CreateProduct_WithInlineData_ShouldCreate(string name, decimal price)
    {
        // Arrange
        var product = new Product { Name = name, Price = price };

        // Act
        var result = await _productService.CreateProductAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(price, result.Price);
        Assert.Equal(DateTimeProvider.Now(), result.CreatedAt);
    }

    [Fact]
    public async Task CreateProduct_WithBogusData_ShouldCreate()
    {
        // Arrange
        var fakeProduct = new Product
        {
            Name = BaseFaker.Commerce.ProductName(),
            Price = BaseFaker.Finance.Amount(10, 5000)
        };

        // Act
        var result = await _productService.CreateProductAsync(fakeProduct);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(fakeProduct.Name, result.Name);
        Assert.Equal(fakeProduct.Price, result.Price);
    }

    [Fact]
    public async Task CreateMultipleProducts_WithBogusData_ShouldCreateAll()
    {
        // Arrange
        var fakeProducts = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            fakeProducts.Add(new Product
            {
                Name = BaseFaker.Commerce.ProductName(),
                Price = BaseFaker.Finance.Amount(10, 5000)
            });
        }

        // Act
        foreach (var product in fakeProducts)
        {
            await _productService.CreateProductAsync(product);
        }

        // Assert
        var allProducts = await _productService.GetAllProductsAsync();
        Assert.Equal(10, allProducts.Count());
    }

    [Fact]
    public async Task CreateProduct_WithNullProduct_ShouldThrowException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _productService.CreateProductAsync(null));
    }

    #endregion

    #region Update Product Feature

    [Fact]
    public async Task UpdateProduct_ShouldSetUpdatedAt()
    {
        // Arrange
        var product = new Product
        {
            Name = BaseFaker.Commerce.ProductName(),
            Price = BaseFaker.Finance.Amount(100, 1000)
        };
        var created = await _productService.CreateProductAsync(product);

        // Simulate time passing - advance the clock by 1 hour
        DateTimeProvider.DateTime = DateTimeProvider.DateTime.AddHours(1);

        // Act
        created.Name = "Updated Name";
        var updated = await _productService.UpdateProductAsync(created);

        // Assert
        Assert.Equal("Updated Name", updated.Name);
        Assert.NotNull(updated.UpdatedAt);
        Assert.True(updated.UpdatedAt > updated.CreatedAt);
    }

    [Fact]
    public async Task UpdateProduct_WithNullProduct_ShouldThrowException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _productService.UpdateProductAsync(null));
    }

    #endregion

    #region Get Product Feature

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task GetProductById_WithInvalidId_ShouldThrowException(int invalidId)
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => _productService.GetProductByIdAsync(invalidId));
    }

    [Fact]
    public async Task GetAllProducts_WithSeededData_ShouldReturnThreeProducts()
    {
        // Arrange
        var context = CreateDbContextWithSeedData();
        var productService = new ProductService(context, DateTimeProvider);

        // Act
        var products = await productService.GetAllProductsAsync();

        // Assert
        Assert.Equal(3, products.Count());
        Assert.Contains(products, p => p.Name == "Laptop");
        Assert.Contains(products, p => p.Name == "Mouse");
        Assert.Contains(products, p => p.Name == "Keyboard");
    }

    [Theory]
    [InlineData(1, "Laptop", 1500)]
    [InlineData(2, "Mouse", 25)]
    [InlineData(3, "Keyboard", 75)]
    public async Task GetProductById_WithSeededData_ShouldReturnCorrectProduct(
        int id, string expectedName, decimal expectedPrice)
    {
        // Arrange
        var context = CreateDbContextWithSeedData();
        var productService = new ProductService(context, DateTimeProvider);

        // Act
        var product = await productService.GetProductByIdAsync(id);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(expectedName, product.Name);
        Assert.Equal(expectedPrice, product.Price);
    }

    #endregion
}
