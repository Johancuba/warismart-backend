using WariSmart.API.Sales.Domain.Model.Aggregates;
using CatchUpPlatform.API.Shared.Domain.Repositories;

namespace WariSmart.API.Sales.Domain.Repositories;

/// <summary>
/// Sale repository contract
/// </summary>
public interface ISaleRepository : IBaseRepository<Sale>
{
    /// <summary>
    /// Find sales by date range
    /// </summary>
    Task<IEnumerable<Sale>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Find today's sales
    /// </summary>
    Task<IEnumerable<Sale>> FindTodayAsync();

    /// <summary>
    /// Get sale with items included
    /// </summary>
    Task<Sale?> FindByIdWithItemsAsync(int idVenta);
}
