using API.Contract.DTOs.Storage.BrandImages;

namespace API.Contract.DTOs.Catalog.Brands
{
    public class BrandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProductCount { get; set; }
        public BrandImageResponse? PrimaryImage { get; set; }
    }
}
