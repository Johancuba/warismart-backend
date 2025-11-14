namespace WariSmart.API.Sales.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a sale
/// </summary>
public record SaleResource(
    int IdVenta,
    string Cliente,
    string DNIRUC,
    string MetodoPago,
    decimal TotalVenta,
    DateTime FechaVenta,
    List<SaleItemDetailResource> Items
);

/// <summary>
/// Resource representing a sale item detail
/// </summary>
public record SaleItemDetailResource(
    int IdItem,
    int IdProducto,
    string NombreProducto,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Subtotal
);
