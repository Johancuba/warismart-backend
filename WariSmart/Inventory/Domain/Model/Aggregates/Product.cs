using WariSmart.API.Inventory.Domain.Model.Commands;

namespace WariSmart.API.Inventory.Domain.Model.Aggregates;

/// <summary>
/// Product Aggregate
/// Represents a product in the retail inventory system
/// </summary>
public partial class Product
{
    protected Product()
    {
        NombreProducto = string.Empty;
        SKU = string.Empty;
        Categoria = string.Empty;
        Ubicacion = string.Empty;
        Estado = string.Empty;
    }

    /// <summary>
    /// Constructor for the Product aggregate.
    /// </summary>
    /// <param name="command">The CreateProductCommand</param>
    public Product(CreateProductCommand command)
    {
        NombreProducto = command.NombreProducto;
        SKU = command.SKU;
        Categoria = command.Categoria;
        StockActual = command.StockActual;
        StockMinimo = command.StockMinimo;
        Ubicacion = command.Ubicacion;
        Precio = command.Precio;
        Estado = command.Estado;
    }

    public int IdProducto { get; }
    public string NombreProducto { get; private set; }
    public string SKU { get; private set; }
    public string Categoria { get; private set; }
    public int StockActual { get; private set; }
    public int StockMinimo { get; private set; }
    public string Ubicacion { get; private set; }
    public decimal Precio { get; private set; }
    public string Estado { get; private set; }

    /// <summary>
    /// Updates the product information
    /// </summary>
    public void Update(UpdateProductCommand command)
    {
        NombreProducto = command.NombreProducto;
        Categoria = command.Categoria;
        StockActual = command.StockActual;
        StockMinimo = command.StockMinimo;
        Ubicacion = command.Ubicacion;
        Precio = command.Precio;
        Estado = command.Estado;
    }

    /// <summary>
    /// Updates the stock quantity
    /// </summary>
    public void UpdateStock(int quantity)
    {
        StockActual = quantity;
    }

    /// <summary>
    /// Checks if stock is below minimum threshold
    /// </summary>
    public bool IsLowStock() => StockActual <= StockMinimo;
}
