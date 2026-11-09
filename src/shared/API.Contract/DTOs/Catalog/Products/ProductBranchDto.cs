using API.Domain.Common.Enums;

namespace API.Contract.DTOs.Catalog.Products
{
    public class ProductBranchDto
    {
        public Guid ProductPublicId { get; set; }
        public Guid BranchPublicId { get; set; }
        public string? Currency { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsRecommended { get; set; }
        public StockStatus StockStatus { get; set; }
    }
}

