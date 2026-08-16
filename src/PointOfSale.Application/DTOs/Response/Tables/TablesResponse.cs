using PointOfSale.Application.Extensions;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Response.Tables
{
    public record TablesResponse(
        Guid Id,
        string Name,
        int Number,
        TableStatus Status,
        int Capacity,
        DateTime CreatedAt,
        TableSessionResponse? CurrentSession =  null
    )
    {
        public string StatusDescription => Status.GetDisplayName();
    }
}


