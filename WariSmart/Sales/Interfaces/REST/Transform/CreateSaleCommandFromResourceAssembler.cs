using WariSmart.API.Sales.Domain.Model.Commands;
using WariSmart.API.Sales.Interfaces.REST.Resources;

namespace WariSmart.API.Sales.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert CreateSaleResource to CreateSaleCommand
/// </summary>
public static class CreateSaleCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a CreateSaleResource to a CreateSaleCommand
    /// </summary>
    public static CreateSaleCommand ToCommandFromResource(CreateSaleResource resource)
    {
        var items = resource.Items.Select(i => new SaleItemCommand(
            i.IdProducto,
            i.NombreProducto,
            i.Cantidad,
            i.PrecioUnitario
        )).ToList();

        return new CreateSaleCommand(
            resource.Cliente,
            resource.DNIRUC,
            resource.MetodoPago,
            items
        );
    }
}
