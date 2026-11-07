using API.Contract.DTOs.Storage.Images;

namespace API.Contract.DTOs.Storage.ProductImages
{
    public class ProductImageResponse
    {
        public Guid Id { get; set; }

        public int? DisplayOrder { get; set; }
        public bool? IsPrimary { get; set; }

        public ImageResponse Image { get; set; } = null!;
    }
}
