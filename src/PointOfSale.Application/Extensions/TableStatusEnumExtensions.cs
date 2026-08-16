using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PointOfSale.Application.Extensions
{
    public static class TableStatusEnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name ?? enumValue.ToString();
        }
    }
}
