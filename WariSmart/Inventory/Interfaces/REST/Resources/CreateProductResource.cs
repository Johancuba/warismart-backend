using System.ComponentModel.DataAnnotations;

namespace WariSmart.API.Inventory.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new product
/// </summary>
public record CreateProductResource(
    [Required] string NombreProducto,
    [Required] string SKU,
    [Required] string Categoria,
    [Required] int StockActual,
    [Required] int StockMinimo,
    [Required] string Ubicacion,
    [Required] decimal Precio,
    [Required] string Estado
);
