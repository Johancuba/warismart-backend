using System.ComponentModel.DataAnnotations;

namespace WariSmart.API.Inventory.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating a product
/// </summary>
public record UpdateProductResource(
    [Required] string NombreProducto,
    [Required] string Categoria,
    [Required] int StockActual,
    [Required] int StockMinimo,
    [Required] string Ubicacion,
    [Required] decimal Precio,
    [Required] string Estado
);
