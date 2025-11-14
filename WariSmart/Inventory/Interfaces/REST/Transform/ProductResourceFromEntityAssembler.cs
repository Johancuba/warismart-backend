using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Interfaces.REST.Resources;

namespace WariSmart.API.Inventory.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Product entity to ProductResource
/// </summary>
public static class ProductResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Product entity to a ProductResource
    /// </summary>
    public static ProductResource ToResourceFromEntity(Product entity) =>
        new ProductResource(
            entity.IdProducto,
            entity.NombreProducto,
            entity.SKU,
            entity.Categoria,
            entity.StockActual,
            entity.StockMinimo,
            entity.Ubicacion,
            entity.Precio,
            entity.Estado,
            entity.IsLowStock()
        );
}
