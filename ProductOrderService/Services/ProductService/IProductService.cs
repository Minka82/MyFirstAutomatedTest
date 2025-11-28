namespace Services.ProductService;

using Infrastructure.Models;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(int id);

    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<Product> CreateProductAsync(Product product);
}
