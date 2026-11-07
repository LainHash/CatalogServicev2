using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Storage.Images;

namespace API.Domain.Features.Storage.ProductImages
{
    public partial class ProductImage : AuditableEntity
    {
        public int DisplayOrder { get; private set; }
        public bool IsPrimary { get; private set; }

        public long ProductId { get; private set; }
        public long ImageId { get; private set; }

        public Product Product { get; private set; } = null!;
        public Image Image { get; private set; } = null!;
    }

    public partial class ProductImage
    {
        private ProductImage() { }

        public ProductImage(bool isPrimary, int displayOrder)
        {
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }

        public ProductImage(long productId, long imageId, bool isPrimary, int displayOrder)
        {
            ProductId = productId;
            ImageId = imageId;
            IsPrimary = isPrimary;
            DisplayOrder = displayOrder;
        }

        public static ProductImage Create(long productId, long imageId, bool isPrimary, int displayOrder)
        {
            return new ProductImage(productId, imageId, isPrimary, displayOrder);
        }

        public void RemovePrimary()
        {
            IsPrimary = false;
        }

        public ProductImage SetProduct(long productId)
        {
            ProductId = productId;
            return this;
        }

        public ProductImage SetImage(long imageId)
        {
            ImageId = imageId;
            return this;
        }
    }
}
