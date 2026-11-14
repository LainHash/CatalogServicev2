using API.Contract.DTOs.Storage.ProductImages;
using API.Domain.Common.Enums;

namespace API.Contract.DTOs.Catalog.Products
{
    public class ProductResponse : SoftDeletableResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string InventoryType { get; set; } = null!;

        public string? BrandName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string UnitSymbol { get; set; } = string.Empty;

        public decimal? UnitPrice { get; set; }
        public string? Currency { get; set; }

        public StockStatus? StockStatus { get; set; }
        public bool? IsRecommended { get; set; }
        public ProductImageResponse? PrimaryImage { get; set; }
    }
}
