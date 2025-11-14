namespace WariSmart.API.Reports.Domain.Model.ValueObjects;

/// <summary>
/// Value object representing daily sales report data
/// </summary>
public record DailySalesReport(
    DateTime Fecha,
    int TotalVentas,
    decimal MontoTotal,
    int ProductosVendidos
);
