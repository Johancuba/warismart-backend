using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Commands;
using WariSmart.API.Sales.Domain.Repositories;
using WariSmart.API.Sales.Domain.Services;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace WariSmart.API.Sales.Application.Internal.CommandServices;

/// <summary>
/// Sale command service implementation
/// </summary>
public class SaleCommandService(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    : ISaleCommandService
{
    /// <inheritdoc />
    public async Task<Sale?> Handle(CreateSaleCommand command)
    {
        var sale = new Sale(command);

        // Add items to the sale
        foreach (var item in command.Items)
        {
            sale.AddItem(
                item.IdProducto,
                item.NombreProducto,
                item.Cantidad,
                item.PrecioUnitario
            );
        }

        try
        {
            await saleRepository.AddAsync(sale);
            await unitOfWork.CompleteAsync();
            return sale;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating sale: {e.Message}");
            return null;
        }
    }
}
