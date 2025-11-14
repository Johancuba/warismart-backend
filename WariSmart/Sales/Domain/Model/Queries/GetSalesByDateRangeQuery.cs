namespace WariSmart.API.Sales.Domain.Model.Queries;

/// <summary>
/// Query to get sales by date range
/// </summary>
public record GetSalesByDateRangeQuery(DateTime StartDate, DateTime EndDate);
