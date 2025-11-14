namespace WariSmart.API.Inventory.Domain.Model.Queries;

/// <summary>
/// Query to get a product by SKU
/// </summary>
public record GetProductBySKUQuery(string SKU);
