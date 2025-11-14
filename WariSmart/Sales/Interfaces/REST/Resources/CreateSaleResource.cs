using System.ComponentModel.DataAnnotations;

namespace WariSmart.API.Sales.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new sale
/// </summary>
public record CreateSaleResource(
    [Required] string Cliente,
    [Required] string DNIRUC,
    [Required] string MetodoPago,
    [Required] List<SaleItemResource> Items
);

/// <summary>
/// Resource for sale item
/// </summary>
public record SaleItemResource(
    [Required] int IdProducto,
    [Required] string NombreProducto,
    [Required] int Cantidad,
    [Required] decimal PrecioUnitario
);
