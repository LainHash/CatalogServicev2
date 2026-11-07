namespace API.Contract.DTOs.Catalog.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string InventoryType { get; set; } = null!;

        public string? BrandName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string UnitSymbol { get; set; } = string.Empty;

        //public decimal? UnitPrice { get; set; }
        //public string? Currency { get; set; } = string.Empty;

        //public StockStatus? StockStatus { get; set; }
        //public bool? IsRecommended { get; set; } = null;
        //public ProductImageResponse? PrimaryImage { get; set; } = null;
    }
}
