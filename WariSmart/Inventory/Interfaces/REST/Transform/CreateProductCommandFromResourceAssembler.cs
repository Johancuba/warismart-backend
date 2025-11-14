using WariSmart.API.Inventory.Domain.Model.Commands;
using WariSmart.API.Inventory.Interfaces.REST.Resources;

namespace WariSmart.API.Inventory.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert CreateProductResource to CreateProductCommand
/// </summary>
public static class CreateProductCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a CreateProductResource to a CreateProductCommand
    /// </summary>
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource) =>
        new CreateProductCommand(
            resource.NombreProducto,
            resource.SKU,
            resource.Categoria,
            resource.StockActual,
            resource.StockMinimo,
            resource.Ubicacion,
            resource.Precio,
            resource.Estado
        );
}
