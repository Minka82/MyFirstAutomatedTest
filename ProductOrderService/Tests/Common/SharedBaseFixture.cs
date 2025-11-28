namespace Tests.Common;

using Bogus;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Services.DateTimeProvider;

/// <summary>
/// Shared base fixture for all tests
/// </summary>
public class SharedBaseFixture : IDisposable
{
    #region Properties

    public AppDbContext AppDbContext { get; private set; }

    public Faker BaseFaker { get; private set; }

    public FixedDateTimeProvider DateTimeProvider { get; private set; }

    #endregion

    #region Constructors 

    public SharedBaseFixture()
    {
        // Initialize In-memory DbContext
        AppDbContext = CreateInMemoryDbContext();

        // Initialize Faker - generator of fake data, so you don't have to think about it.
        BaseFaker = new Faker();

        //  Initialize DateTimeProvider with fixed date /testing purposes only/
        DateTimeProvider = new FixedDateTimeProvider
        {
            DateTime = new DateTime(2025, 12, 1, 12, 0, 0, DateTimeKind.Utc)
        };
    }

    #endregion

    #region DbContextMethods

    /// <summary>
    ///  Creates a new InMemory DbContext (isolated per test)
    /// </summary>
    public AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        return new AppDbContext(options);
    }

    /// <summary>
    ///  Creates DbContext with seeded data
    /// </summary>
    public AppDbContext CreateDbContextWithSeedData()
    {
        var context = CreateInMemoryDbContext();
        SeedDatabase(context);
        return context;
    }

    /// <summary>
    /// Seeds database with standard test data
    /// </summary>
    private void SeedDatabase(AppDbContext context)
    {
        var products = new[]
        {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1500m,
                    CreatedAt = DateTimeProvider.Now()
                },
                new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 25m,
                    CreatedAt = DateTimeProvider.Now()
                },
                new Product
                {
                    Id = 3,
                    Name = "Keyboard",
                    Price = 75m,
                    CreatedAt = DateTimeProvider.Now()
                }
            };

        context.Products.AddRange(products);
        context.SaveChanges();
    }

    /// <summary>
    /// Clear all data from database
    /// </summary>
    public void ClearDatabase()
    {
        AppDbContext.Orders.RemoveRange(AppDbContext.Orders);
        AppDbContext.Products.RemoveRange(AppDbContext.Products);
        AppDbContext.SaveChanges();
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        AppDbContext?.Dispose();
    }

    #endregion
}
