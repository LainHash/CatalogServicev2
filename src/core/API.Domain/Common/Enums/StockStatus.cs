using System.Text.Json.Serialization;

namespace API.Domain.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StockStatus
    {
        OutOfStock,
        LowStock,
        Available
    }
}

