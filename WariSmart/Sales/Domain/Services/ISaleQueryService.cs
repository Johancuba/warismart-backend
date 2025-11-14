using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Queries;

namespace WariSmart.API.Sales.Domain.Services;

/// <summary>
/// Sale query service interface
/// </summary>
public interface ISaleQueryService
{
    /// <summary>
    /// Handle get all sales query
    /// </summary>
    Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query);

    /// <summary>
    /// Handle get sale by id query
    /// </summary>
    Task<Sale?> Handle(GetSaleByIdQuery query);

    /// <summary>
    /// Handle get sales by date range query
    /// </summary>
    Task<IEnumerable<Sale>> Handle(GetSalesByDateRangeQuery query);

    /// <summary>
    /// Handle get today's sales query
    /// </summary>
    Task<IEnumerable<Sale>> Handle(GetSalesTodayQuery query);
}
