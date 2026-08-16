namespace PointOfSale.Application.DTOs.Request.Tables
{
    public record UpdateTableRequest(
          string Name,
          string Description,
          int NumberOfChairs,
          int Status
    );
}
