using API.Domain.Common.Entities;
using API.Domain.Features.Storage.BrandImages;
using API.Domain.Features.Storage.ProductImages;

namespace API.Domain.Features.Storage.Images
{
    public partial class Image : AuditableEntity
    {
        public string AltText { get; private set; } = null!;

        public string Url { get; private set; } = null!;
        public string StoragePath { get; private set; } = null!;

        public ProductImage? ProductImage { get; private set; } = null!;
        public BrandImage? BrandImage { get; private set; } = null!;
    }

    public partial class Image
    {
        private Image() { }

        public Image(string altText, string url, string storagePath)
        {
            AltText = altText;
            Url = url;
            StoragePath = storagePath;
        }

        public Image SetAltText(string altText)
        {
            AltText = altText;
            return this;
        }

        public static Image Create(string altText, string url, string storagePath)
        {
            return new Image(altText, url, storagePath);
        }
    }
}
