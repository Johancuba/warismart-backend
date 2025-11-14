using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Domain.Model.Commands;

namespace WariSmart.API.Inventory.Domain.Services;

/// <summary>
/// Product command service interface
/// </summary>
public interface IProductCommandService
{
    /// <summary>
    /// Handle create product command
    /// </summary>
    Task<Product?> Handle(CreateProductCommand command);

    /// <summary>
    /// Handle update product command
    /// </summary>
    Task<Product?> Handle(UpdateProductCommand command);

    /// <summary>
    /// Handle delete product command
    /// </summary>
    Task<bool> Handle(DeleteProductCommand command);
}
