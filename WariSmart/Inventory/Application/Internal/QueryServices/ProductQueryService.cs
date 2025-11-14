using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Domain.Model.Queries;
using WariSmart.API.Inventory.Domain.Repositories;
using WariSmart.API.Inventory.Domain.Services;

namespace WariSmart.API.Inventory.Application.Internal.QueryServices;

/// <summary>
/// Product query service implementation
/// </summary>
public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query)
    {
        return await productRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.FindByIdAsync(query.IdProducto);
    }

    /// <inheritdoc />
    public async Task<Product?> Handle(GetProductBySKUQuery query)
    {
        return await productRepository.FindBySKUAsync(query.SKU);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Handle(GetProductsByCategoryQuery query)
    {
        return await productRepository.FindByCategoryAsync(query.Categoria);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> Handle(GetLowStockProductsQuery query)
    {
        return await productRepository.FindLowStockProductsAsync();
    }
}
