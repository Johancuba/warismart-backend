namespace WariSmart.API.Inventory.Domain.Model.Commands;

/// <summary>
/// Command to update an existing product
/// </summary>
public record UpdateProductCommand(
    int IdProducto,
    string NombreProducto,
    string Categoria,
    int StockActual,
    int StockMinimo,
    string Ubicacion,
    decimal Precio,
    string Estado
);
