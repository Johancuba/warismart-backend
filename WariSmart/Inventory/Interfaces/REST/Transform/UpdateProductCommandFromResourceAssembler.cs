using WariSmart.API.Inventory.Domain.Model.Commands;
using WariSmart.API.Inventory.Interfaces.REST.Resources;

namespace WariSmart.API.Inventory.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert UpdateProductResource to UpdateProductCommand
/// </summary>
public static class UpdateProductCommandFromResourceAssembler
{
    /// <summary>
    /// Converts an UpdateProductResource to an UpdateProductCommand
    /// </summary>
    public static UpdateProductCommand ToCommandFromResource(int idProducto, UpdateProductResource resource) =>
        new UpdateProductCommand(
            idProducto,
            resource.NombreProducto,
            resource.Categoria,
            resource.StockActual,
            resource.StockMinimo,
            resource.Ubicacion,
            resource.Precio,
            resource.Estado
        );
}
