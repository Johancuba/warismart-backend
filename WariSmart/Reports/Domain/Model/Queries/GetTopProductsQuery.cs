namespace WariSmart.API.Reports.Domain.Model.Queries;

public record GetTopProductsQuery(DateTime Date, int TopCount = 10);
