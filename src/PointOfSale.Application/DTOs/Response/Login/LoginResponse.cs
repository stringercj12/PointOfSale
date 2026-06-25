using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Response.Login
{
    public record LoginResponse(
        string Token,           
        string TokenType,     
        long ExpiresIn,        
        UserSessionInfo User   
    );

    public record UserSessionInfo(
        int Id,
        string Name,
        string Email,
        RoleEnum Role,         
        string? ProfileImage  
    );
}
