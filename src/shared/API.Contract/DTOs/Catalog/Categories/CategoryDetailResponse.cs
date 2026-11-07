using API.Contract.DTOs.Catalog.Products;

namespace API.Contract.DTOs.Catalog.Categories
{
    public class CategoryDetailResponse : CategoryResponse
    {
        //public IEnumerable<ImageResponse> Images { get; set; } = [];
        public IEnumerable<ProductMinimalResponse> Products { get; set; } = [];
    }
}
