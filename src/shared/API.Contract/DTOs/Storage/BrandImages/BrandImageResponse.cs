using API.Contract.DTOs.Storage.Images;

namespace API.Contract.DTOs.Storage.BrandImages
{
    public class BrandImageResponse
    {
        public Guid Id { get; set; }

        public int? DisplayOrder { get; set; }
        public bool? IsPrimary { get; set; }

        public ImageResponse Image { get; set; } = null!;
    }
}
