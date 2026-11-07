using API.Contract.DTOs.Catalog.Products;

namespace API.Contract.DTOs.Catalog.Brands
{
    public class BrandDetailResponse : BrandResponse
    {
        public IEnumerable<ProductMinimalResponse> Products { get; set; } = [];
    }
}
