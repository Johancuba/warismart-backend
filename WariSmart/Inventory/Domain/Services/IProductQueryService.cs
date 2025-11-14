using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Domain.Model.Queries;

namespace WariSmart.API.Inventory.Domain.Services;

/// <summary>
/// Product query service interface
/// </summary>
public interface IProductQueryService
{
    /// <summary>
    /// Handle get all products query
    /// </summary>
    Task<IEnumerable<Product>> Handle(GetAllProductsQuery query);

    /// <summary>
    /// Handle get product by id query
    /// </summary>
    Task<Product?> Handle(GetProductByIdQuery query);

    /// <summary>
    /// Handle get product by SKU query
    /// </summary>
    Task<Product?> Handle(GetProductBySKUQuery query);

    /// <summary>
    /// Handle get products by category query
    /// </summary>
    Task<IEnumerable<Product>> Handle(GetProductsByCategoryQuery query);

    /// <summary>
    /// Handle get low stock products query
    /// </summary>
    Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query);
}
