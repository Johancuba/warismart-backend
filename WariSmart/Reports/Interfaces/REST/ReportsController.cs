using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WariSmart.API.Reports.Domain.Model.Queries;
using WariSmart.API.Reports.Domain.Model.ValueObjects;
using WariSmart.API.Reports.Domain.Services;

namespace WariSmart.API.Reports.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Reports")]
public class ReportsController(IReportQueryService reportQueryService) : ControllerBase
{
    /// <summary>
    /// Gets today's sales report
    /// </summary>
    [HttpGet("today")]
    [SwaggerOperation(
        Summary = "Gets today's sales report",
        Description = "Retrieves sales statistics for today",
        OperationId = "GetTodaySalesReport")]
    [SwaggerResponse(200, "Report generated", typeof(DailySalesReport))]
    public async Task<ActionResult> GetTodaySalesReport()
    {
        var query = new GetTodaySalesReportQuery();
        var result = await reportQueryService.Handle(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets daily sales report for a specific date
    /// </summary>
    [HttpGet("daily")]
    [SwaggerOperation(
        Summary = "Gets daily sales report",
        Description = "Retrieves sales statistics for a specific date",
        OperationId = "GetDailySalesReport")]
    [SwaggerResponse(200, "Report generated", typeof(DailySalesReport))]
    public async Task<ActionResult> GetDailySalesReport([FromQuery] DateTime date)
    {
        var query = new GetDailySalesReportQuery(date);
        var result = await reportQueryService.Handle(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets top selling products for a specific date
    /// </summary>
    [HttpGet("top-products")]
    [SwaggerOperation(
        Summary = "Gets top selling products",
        Description = "Retrieves the most sold products for a specific date",
        OperationId = "GetTopProducts")]
    [SwaggerResponse(200, "Report generated", typeof(IEnumerable<TopProductReport>))]
    public async Task<ActionResult> GetTopProducts([FromQuery] DateTime date, [FromQuery] int top = 10)
    {
        var query = new GetTopProductsQuery(date, top);
        var result = await reportQueryService.Handle(query);
        return Ok(result);
    }
}
