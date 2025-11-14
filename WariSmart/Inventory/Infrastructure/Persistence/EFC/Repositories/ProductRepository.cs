using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WariSmart.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Product repository implementation
/// </summary>
public class ProductRepository(AppDbContext context)
    : BaseRepository<Product>(context), IProductRepository
{
    /// <inheritdoc />
    public async Task<Product?> FindBySKUAsync(string sku)
    {
        return await Context.Set<Product>()
            .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> FindByCategoryAsync(string categoria)
    {
        return await Context.Set<Product>()
            .Where(p => p.Categoria == categoria)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Product>> FindLowStockProductsAsync()
    {
        return await Context.Set<Product>()
            .Where(p => p.StockActual <= p.StockMinimo)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsBySKUAsync(string sku)
    {
        return await Context.Set<Product>()
            .AnyAsync(p => p.SKU == sku);
    }
}
