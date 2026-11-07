using System.Text.Json.Serialization;

namespace API.Domain.Features.Catalog.Products.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InventoryType
    {
        MadeToOrder,
        StockTracked
    }
}
