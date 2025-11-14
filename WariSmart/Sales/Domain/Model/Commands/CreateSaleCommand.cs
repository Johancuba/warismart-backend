namespace WariSmart.API.Sales.Domain.Model.Commands;

/// <summary>
/// Command to create a new sale
/// </summary>
public record CreateSaleCommand(
    string Cliente,
    string DNIRUC,
    string MetodoPago,
    List<SaleItemCommand> Items
);

/// <summary>
/// Sale item data for creating a sale
/// </summary>
public record SaleItemCommand(
    int IdProducto,
    string NombreProducto,
    int Cantidad,
    decimal PrecioUnitario
);
