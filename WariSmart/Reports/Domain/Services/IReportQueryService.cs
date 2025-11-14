using WariSmart.API.Reports.Domain.Model.Queries;
using WariSmart.API.Reports.Domain.Model.ValueObjects;

namespace WariSmart.API.Reports.Domain.Services;

public interface IReportQueryService
{
    Task<DailySalesReport?> Handle(GetDailySalesReportQuery query);
    Task<DailySalesReport?> Handle(GetTodaySalesReportQuery query);
    Task<IEnumerable<TopProductReport>> Handle(GetTopProductsQuery query);
}
