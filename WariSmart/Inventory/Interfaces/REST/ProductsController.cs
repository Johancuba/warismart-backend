using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WariSmart.API.Inventory.Domain.Model.Commands;
using WariSmart.API.Inventory.Domain.Model.Queries;
using WariSmart.API.Inventory.Domain.Services;
using WariSmart.API.Inventory.Interfaces.REST.Resources;
using WariSmart.API.Inventory.Interfaces.REST.Transform;

namespace WariSmart.API.Inventory.Interfaces.REST;

/// <summary>
/// Products controller for inventory management
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Products")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService)
    : ControllerBase
{
    /// <summary>
    /// Creates a new product
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new product",
        Description = "Creates a new product in the inventory system",
        OperationId = "CreateProduct")]
    [SwaggerResponse(201, "The product was created", typeof(ProductResource))]
    [SwaggerResponse(400, "The product data is invalid")]
    [SwaggerResponse(409, "A product with the same SKU already exists")]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        try
        {
            var result = await productCommandService.Handle(createProductCommand);
            if (result is null) return BadRequest("Failed to create product");
            
            return CreatedAtAction(
                nameof(GetProductById), 
                new { id = result.IdProducto }, 
                ProductResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict($"Product with SKU {resource.SKU} already exists");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all products",
        Description = "Retrieves all products from the inventory",
        OperationId = "GetAllProducts")]
    [SwaggerResponse(200, "Products retrieved successfully", typeof(IEnumerable<ProductResource>))]
    public async Task<ActionResult> GetAllProducts()
    {
        var getAllProductsQuery = new GetAllProductsQuery();
        var result = await productQueryService.Handle(getAllProductsQuery);
        var resources = result.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a product by ID",
        Description = "Retrieves a specific product by its identifier",
        OperationId = "GetProductById")]
    [SwaggerResponse(200, "Product found", typeof(ProductResource))]
    [SwaggerResponse(404, "Product not found")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var getProductByIdQuery = new GetProductByIdQuery(id);
        var result = await productQueryService.Handle(getProductByIdQuery);
        
        if (result is null) return NotFound($"Product with ID {id} not found");
        
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    /// <summary>
    /// Gets a product by SKU
    /// </summary>
    [HttpGet("sku/{sku}")]
    [SwaggerOperation(
        Summary = "Gets a product by SKU",
        Description = "Retrieves a specific product by its SKU code",
        OperationId = "GetProductBySKU")]
    [SwaggerResponse(200, "Product found", typeof(ProductResource))]
    [SwaggerResponse(404, "Product not found")]
    public async Task<ActionResult> GetProductBySKU(string sku)
    {
        var getProductBySKUQuery = new GetProductBySKUQuery(sku);
        var result = await productQueryService.Handle(getProductBySKUQuery);
        
        if (result is null) return NotFound($"Product with SKU {sku} not found");
        
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    /// <summary>
    /// Gets products by category
    /// </summary>
    [HttpGet("category/{category}")]
    [SwaggerOperation(
        Summary = "Gets products by category",
        Description = "Retrieves all products belonging to a specific category",
        OperationId = "GetProductsByCategory")]
    [SwaggerResponse(200, "Products retrieved successfully", typeof(IEnumerable<ProductResource>))]
    public async Task<ActionResult> GetProductsByCategory(string category)
    {
        var getProductsByCategoryQuery = new GetProductsByCategoryQuery(category);
        var result = await productQueryService.Handle(getProductsByCategoryQuery);
        var resources = result.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Gets products with low stock
    /// </summary>
    [HttpGet("low-stock")]
    [SwaggerOperation(
        Summary = "Gets products with low stock",
        Description = "Retrieves all products where stock is at or below minimum threshold",
        OperationId = "GetLowStockProducts")]
    [SwaggerResponse(200, "Products retrieved successfully", typeof(IEnumerable<ProductResource>))]
    public async Task<ActionResult> GetLowStockProducts()
    {
        var getLowStockProductsQuery = new GetLowStockProductsQuery();
        var result = await productQueryService.Handle(getLowStockProductsQuery);
        var resources = result.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Updates a product
    /// </summary>
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Updates a product",
        Description = "Updates an existing product's information",
        OperationId = "UpdateProduct")]
    [SwaggerResponse(200, "Product updated successfully", typeof(ProductResource))]
    [SwaggerResponse(400, "Invalid product data")]
    [SwaggerResponse(404, "Product not found")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProductResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updateProductCommand = UpdateProductCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        
        try
        {
            var result = await productCommandService.Handle(updateProductCommand);
            if (result is null) return BadRequest("Failed to update product");
            
            var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(productResource);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Deletes a product",
        Description = "Removes a product from the inventory",
        OperationId = "DeleteProduct")]
    [SwaggerResponse(204, "Product deleted successfully")]
    [SwaggerResponse(404, "Product not found")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var deleteProductCommand = new DeleteProductCommand(id);
        var result = await productCommandService.Handle(deleteProductCommand);
        
        if (!result) return NotFound($"Product with ID {id} not found");
        
        return NoContent();
    }
}
