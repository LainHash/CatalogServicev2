using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Brands.Enums;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Storage;
using API.Domain.Features.Storage.BrandImages;

namespace API.Domain.Features.Catalog.Brands
{
    public class Brand : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public BrandType Type { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
        public ICollection<BrandImage> BrandImages { get; private set; } = [];
    }
}
