namespace Services.ProductService;

using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Services.DateTimeProvider;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ProductService(AppDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentNullException("Product id must be greater than 0!");
        }

        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException("Product cannot be null!");
        }

        product.CreatedAt = _dateTimeProvider.Now();
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException("Product cannot be null!");
        }

        product.UpdatedAt = _dateTimeProvider.Now();
        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return product;
    }
}