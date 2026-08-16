using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PointOfSale.Domain.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TableStatus
    {
        [Display(Name = "Indisponível")]
        Unavailable = 0,

        [Display(Name = "Disponível")]  
        Free = 1,

        [Display(Name = "Ocupada")]
        Occupied = 2,

        [Display(Name = "Em Fechamento")]
        Closed = 3,

        [Display(Name = "Reservada")]
        Reserved = 4
    }
}
