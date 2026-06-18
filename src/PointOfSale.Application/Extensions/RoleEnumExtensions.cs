using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.Extensions
{
    public static class RoleEnumExtensions
    {
        public static string GetDescription(this RoleEnum role) => role switch
        {
            RoleEnum.ADMIN => "Administrador",
            RoleEnum.CLIENT => "Cliente",
            RoleEnum.OPERATION => "Operação",
            _ => role.ToString()
        };
    }
}
