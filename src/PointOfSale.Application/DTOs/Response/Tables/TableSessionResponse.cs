namespace PointOfSale.Application.DTOs.Response.Tables
{
    public record TableSessionResponse(
         Guid OrderId,
         DateTime OpenedAt,
         string? SellerName,
         string? CustomerName,
         decimal TotalAmount
    );
}
