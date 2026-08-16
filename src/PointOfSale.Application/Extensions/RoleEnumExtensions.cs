using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.Extensions;

public static class RoleEnumExtensions
{
    public static string GetDescription(this Role role)
    {
        return role switch
        {
            Role.ADMIN => "Administrador",
            Role.CLIENT => "Cliente",
            Role.OPERATION => "Operação",
            _ => role.ToString()
        };
    }
}