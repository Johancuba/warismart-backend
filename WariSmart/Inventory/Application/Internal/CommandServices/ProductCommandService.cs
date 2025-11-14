using WariSmart.API.Inventory.Domain.Model.Aggregates;
using WariSmart.API.Inventory.Domain.Model.Commands;
using WariSmart.API.Inventory.Domain.Repositories;
using WariSmart.API.Inventory.Domain.Services;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace WariSmart.API.Inventory.Application.Internal.CommandServices;

/// <summary>
/// Product command service implementation
/// </summary>
public class ProductCommandService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    : IProductCommandService
{
    /// <inheritdoc />
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        // Check if product with same SKU already exists
        if (await productRepository.ExistsBySKUAsync(command.SKU))
            throw new Exception($"Product with SKU {command.SKU} already exists");

        var product = new Product(command);
        
        try
        {
            await productRepository.AddAsync(product);
            await unitOfWork.CompleteAsync();
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating product: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Product?> Handle(UpdateProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.IdProducto);
        
        if (product == null)
            throw new Exception($"Product with ID {command.IdProducto} not found");

        product.Update(command);

        try
        {
            productRepository.Update(product);
            await unitOfWork.CompleteAsync();
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating product: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteProductCommand command)
    {
        var product = await productRepository.FindByIdAsync(command.IdProducto);
        
        if (product == null)
            return false;

        try
        {
            productRepository.Remove(product);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting product: {e.Message}");
            return false;
        }
    }
}
