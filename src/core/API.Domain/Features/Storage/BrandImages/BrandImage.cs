using API.Domain.Features.Catalog.Brands;
using API.Domain.Common.Entities;
using API.Domain.Features.Storage.Images;

namespace API.Domain.Features.Storage.BrandImages
{
    public partial class BrandImage : AuditableEntity
    {
        public int DisplayOrder { get; private set; }
        public bool IsPrimary { get; private set; }

        public long BrandId { get; private set; }
        public long ImageId { get; private set; }

        public Brand Brand { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class BrandImage
    {
        private BrandImage() { }

        public BrandImage(bool isPrimary, int displayOrder)
        {
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }

        public BrandImage(long brandId, long imageId)
        {
            BrandId = brandId;
            ImageId = imageId;
        }

        public BrandImage(long brandId, long imageId, bool isPrimary, int displayOrder)
        {
            BrandId = brandId;
            ImageId = imageId;
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }

        public static BrandImage Create(long brandId, long imageId)
        {
            return new BrandImage(brandId, imageId);
        }

        public static BrandImage Create(long brandId, long imageId, bool isPrimary, int displayOrder)
        {
            return new BrandImage(brandId, imageId, isPrimary, displayOrder);
        }

        public void RemovePrimary()
        {
            IsPrimary = false;
        }

        public BrandImage SetBrand(long brandId)
        {
            BrandId = brandId;
            return this;
        }

        public BrandImage SetImage(long imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
