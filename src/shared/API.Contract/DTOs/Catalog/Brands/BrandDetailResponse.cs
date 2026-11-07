using API.Contract.DTOs.Catalog.Products;
using API.Contract.DTOs.Storage.BrandImages;

namespace API.Contract.DTOs.Catalog.Brands
{
    public class BrandDetailResponse : BrandResponse
    {
        public IEnumerable<ProductMinimalResponse> Products { get; set; } = [];
        public IEnumerable<BrandImageResponse> BrandImages  { get; set; } = [];
    }
}
