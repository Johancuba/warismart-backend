using WariSmart.API.Inventory.Domain.Model.Aggregates;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace WariSmart.API.Inventory.Domain.Repositories;

/// <summary>
/// Product repository contract
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{
    /// <summary>
    /// Find a product by SKU
    /// </summary>
    Task<Product?> FindBySKUAsync(string sku);

    /// <summary>
    /// Find products by category
    /// </summary>
    Task<IEnumerable<Product>> FindByCategoryAsync(string categoria);

    /// <summary>
    /// Find products with low stock (stock actual <= stock minimo)
    /// </summary>
    Task<IEnumerable<Product>> FindLowStockProductsAsync();

    /// <summary>
    /// Check if a product with the given SKU exists
    /// </summary>
    Task<bool> ExistsBySKUAsync(string sku);
}
