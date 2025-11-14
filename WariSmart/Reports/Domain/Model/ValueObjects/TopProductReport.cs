namespace WariSmart.API.Reports.Domain.Model.ValueObjects;

/// <summary>
/// Value object representing top selling products
/// </summary>
public record TopProductReport(
    int IdProducto,
    string NombreProducto,
    int CantidadVendida,
    decimal MontoTotal
);
