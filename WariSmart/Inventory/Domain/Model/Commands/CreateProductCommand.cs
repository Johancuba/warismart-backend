namespace WariSmart.API.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to create a new product
/// </summary>
public record CreateProductCommand(
    string NombreProducto,
    string SKU,
    string Categoria,
    int StockActual,
    int StockMinimo,
    string Ubicacion,
    decimal Precio,
    string Estado
);
