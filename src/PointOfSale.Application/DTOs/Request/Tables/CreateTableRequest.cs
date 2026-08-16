namespace PointOfSale.Application.DTOs.Request.Tables
{
    public record CreateTableRequest(
          string Name,
          string Description,
          int Number,
          int Capacity
    );
}
