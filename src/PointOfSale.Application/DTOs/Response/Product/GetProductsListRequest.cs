namespace PointOfSale.Application.DTOs.Response.Product;

public record GetProductsListRequest(
    bool? OnlyActive = true,
    string? SearchTerm = null
);