using WariSmart.API.Reports.Domain.Model.Queries;
using WariSmart.API.Reports.Domain.Model.ValueObjects;
using WariSmart.API.Reports.Domain.Services;
using WariSmart.API.Sales.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace WariSmart.API.Reports.Application.Internal.QueryServices;

/// <summary>
/// Report query service implementation
/// Generates reports from sales and product data
/// </summary>
public class ReportQueryService(AppDbContext context, ISaleRepository saleRepository) : IReportQueryService
{
    public async Task<DailySalesReport?> Handle(GetDailySalesReportQuery query)
    {
        var startDate = query.Date.Date;
        var endDate = startDate.AddDays(1);

        var sales = await saleRepository.FindByDateRangeAsync(startDate, endDate);
        var salesList = sales.ToList();

        if (!salesList.Any())
            return new DailySalesReport(query.Date, 0, 0, 0);

        return new DailySalesReport(
            query.Date,
            salesList.Count,
            salesList.Sum(s => s.TotalVenta),
            salesList.SelectMany(s => s.Items).Sum(i => i.Cantidad)
        );
    }

    public async Task<DailySalesReport?> Handle(GetTodaySalesReportQuery query)
    {
        var today = DateTime.UtcNow.Date;
        return await Handle(new GetDailySalesReportQuery(today));
    }

    public async Task<IEnumerable<TopProductReport>> Handle(GetTopProductsQuery query)
    {
        var startDate = query.Date.Date;
        var endDate = startDate.AddDays(1);

        var sales = await saleRepository.FindByDateRangeAsync(startDate, endDate);
        
        var topProducts = sales
            .SelectMany(s => s.Items)
            .GroupBy(i => new { i.IdProducto, i.NombreProducto })
            .Select(g => new TopProductReport(
                g.Key.IdProducto,
                g.Key.NombreProducto,
                g.Sum(i => i.Cantidad),
                g.Sum(i => i.Subtotal)
            ))
            .OrderByDescending(p => p.CantidadVendida)
            .Take(query.TopCount);

        return topProducts.ToList();
    }
}
