namespace WariSmart.API.Sales.Domain.Model.Entities;

/// <summary>
/// Sale Item Entity
/// Represents an individual item within a sale
/// </summary>
public class SaleItem
{
    public int IdItem { get; set; }
    public int IdVenta { get; set; }
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
