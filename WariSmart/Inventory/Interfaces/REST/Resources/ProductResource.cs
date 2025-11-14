namespace WariSmart.API.Inventory.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a product
/// </summary>
public record ProductResource(
    int IdProducto,
    string NombreProducto,
    string SKU,
    string Categoria,
    int StockActual,
    int StockMinimo,
    string Ubicacion,
    decimal Precio,
    string Estado,
    bool IsLowStock
);
