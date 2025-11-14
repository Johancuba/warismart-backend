using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WariSmart.API.Sales.Domain.Model.Queries;
using WariSmart.API.Sales.Domain.Services;
using WariSmart.API.Sales.Interfaces.REST.Resources;
using WariSmart.API.Sales.Interfaces.REST.Transform;

namespace WariSmart.API.Sales.Interfaces.REST;

/// <summary>
/// Sales controller for managing sales transactions
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Sales")]
public class SalesController(
    ISaleCommandService saleCommandService,
    ISaleQueryService saleQueryService)
    : ControllerBase
{
    /// <summary>
    /// Creates a new sale
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new sale",
        Description = "Registers a new sale transaction in the system",
        OperationId = "CreateSale")]
    [SwaggerResponse(201, "The sale was created", typeof(SaleResource))]
    [SwaggerResponse(400, "The sale data is invalid")]
    public async Task<ActionResult> CreateSale([FromBody] CreateSaleResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createSaleCommand = CreateSaleCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        try
        {
            var result = await saleCommandService.Handle(createSaleCommand);
            if (result is null) return BadRequest("Failed to create sale");
            
            return CreatedAtAction(
                nameof(GetSaleById), 
                new { id = result.IdVenta }, 
                SaleResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets all sales
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all sales",
        Description = "Retrieves all sales from the system",
        OperationId = "GetAllSales")]
    [SwaggerResponse(200, "Sales retrieved successfully", typeof(IEnumerable<SaleResource>))]
    public async Task<ActionResult> GetAllSales()
    {
        var getAllSalesQuery = new GetAllSalesQuery();
        var result = await saleQueryService.Handle(getAllSalesQuery);
        var resources = result.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets a sale by ID
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a sale by ID",
        Description = "Retrieves a specific sale by its identifier",
        OperationId = "GetSaleById")]
    [SwaggerResponse(200, "Sale found", typeof(SaleResource))]
    [SwaggerResponse(404, "Sale not found")]
    public async Task<ActionResult> GetSaleById(int id)
    {
        var getSaleByIdQuery = new GetSaleByIdQuery(id);
        var result = await saleQueryService.Handle(getSaleByIdQuery);
        
        if (result is null) return NotFound($"Sale with ID {id} not found");
        
        var resource = SaleResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    /// <summary>
    /// Gets today's sales
    /// </summary>
    [HttpGet("today")]
    [SwaggerOperation(
        Summary = "Gets today's sales",
        Description = "Retrieves all sales made today",
        OperationId = "GetSalesToday")]
    [SwaggerResponse(200, "Sales retrieved successfully", typeof(IEnumerable<SaleResource>))]
    public async Task<ActionResult> GetSalesToday()
    {
        var getSalesTodayQuery = new GetSalesTodayQuery();
        var result = await saleQueryService.Handle(getSalesTodayQuery);
        var resources = result.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets sales by date range
    /// </summary>
    [HttpGet("date-range")]
    [SwaggerOperation(
        Summary = "Gets sales by date range",
        Description = "Retrieves sales within a specific date range",
        OperationId = "GetSalesByDateRange")]
    [SwaggerResponse(200, "Sales retrieved successfully", typeof(IEnumerable<SaleResource>))]
    public async Task<ActionResult> GetSalesByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var getSalesByDateRangeQuery = new GetSalesByDateRangeQuery(startDate, endDate);
        var result = await saleQueryService.Handle(getSalesByDateRangeQuery);
        var resources = result.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}
