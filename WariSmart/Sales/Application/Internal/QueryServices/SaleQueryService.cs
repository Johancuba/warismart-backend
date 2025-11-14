using WariSmart.API.Sales.Domain.Model.Aggregates;
using WariSmart.API.Sales.Domain.Model.Queries;
using WariSmart.API.Sales.Domain.Repositories;
using WariSmart.API.Sales.Domain.Services;

namespace WariSmart.API.Sales.Application.Internal.QueryServices;

/// <summary>
/// Sale query service implementation
/// </summary>
public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query)
    {
        return await saleRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Sale?> Handle(GetSaleByIdQuery query)
    {
        return await saleRepository.FindByIdWithItemsAsync(query.IdVenta);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> Handle(GetSalesByDateRangeQuery query)
    {
        return await saleRepository.FindByDateRangeAsync(query.StartDate, query.EndDate);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Sale>> Handle(GetSalesTodayQuery query)
    {
        return await saleRepository.FindTodayAsync();
    }
}
