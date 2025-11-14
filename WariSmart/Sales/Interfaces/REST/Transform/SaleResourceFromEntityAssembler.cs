using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Interfaces.REST.Resources;

namespace WariSmart.API.Sales.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Sale entity to SaleResource
/// </summary>
public static class SaleResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a Sale entity to a SaleResource
    /// </summary>
    public static SaleResource ToResourceFromEntity(Sale entity)
    {
        var items = entity.Items.Select(i => new SaleItemDetailResource(
            i.IdItem,
            i.IdProducto,
            i.NombreProducto,
            i.Cantidad,
            i.PrecioUnitario,
            i.Subtotal
        )).ToList();

        return new SaleResource(
            entity.IdVenta,
            entity.Cliente,
            entity.DNIRUC,
            entity.MetodoPago,
            entity.TotalVenta,
            entity.FechaVenta,
            items
        );
    }
}
