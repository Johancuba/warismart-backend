using WariSmart.API.Sales.Domain.Model.Commands;
using WariSmart.API.Sales.Domain.Model.Entities;

namespace WariSmart.API.Sales.Domain.Model.Aggregates;

/// <summary>
/// Sale Aggregate
/// Represents a sale transaction in the retail system
/// </summary>
public partial class Sale
{
    protected Sale()
    {
        Cliente = string.Empty;
        DNIRUC = string.Empty;
        MetodoPago = string.Empty;
        Items = new List<SaleItem>();
    }

    /// <summary>
    /// Constructor for the Sale aggregate
    /// </summary>
    public Sale(CreateSaleCommand command)
    {
        Cliente = command.Cliente;
        DNIRUC = command.DNIRUC;
        MetodoPago = command.MetodoPago;
        FechaVenta = DateTime.UtcNow;
        Items = new List<SaleItem>();
        TotalVenta = 0;
    }

    public int IdVenta { get; }
    public string Cliente { get; private set; }
    public string DNIRUC { get; private set; }
    public string MetodoPago { get; private set; }
    public decimal TotalVenta { get; private set; }
    public DateTime FechaVenta { get; private set; }
    public ICollection<SaleItem> Items { get; private set; }

    /// <summary>
    /// Adds an item to the sale
    /// </summary>
    public void AddItem(int idProducto, string nombreProducto, int cantidad, decimal precioUnitario)
    {
        var item = new SaleItem
        {
            IdProducto = idProducto,
            NombreProducto = nombreProducto,
            Cantidad = cantidad,
            PrecioUnitario = precioUnitario,
            Subtotal = cantidad * precioUnitario
        };
        
        Items.Add(item);
        RecalculateTotal();
    }

    /// <summary>
    /// Recalculates the total sale amount
    /// </summary>
    private void RecalculateTotal()
    {
        TotalVenta = Items.Sum(i => i.Subtotal);
    }
}
