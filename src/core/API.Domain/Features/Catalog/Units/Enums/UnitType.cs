using System.Text.Json.Serialization;

namespace API.Domain.Features.Catalog.Units.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UnitType
    {
        Weight,
        Volume,
        Quantity
    }
}
