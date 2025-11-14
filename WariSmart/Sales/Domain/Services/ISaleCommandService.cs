using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Commands;

namespace WariSmart.API.Sales.Domain.Services;

/// <summary>
/// Sale command service interface
/// </summary>
public interface ISaleCommandService
{
    /// <summary>
    /// Handle create sale command
    /// </summary>
    Task<Sale?> Handle(CreateSaleCommand command);
}
