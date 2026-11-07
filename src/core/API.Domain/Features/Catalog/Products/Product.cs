using API.Domain.Features.Catalog.Brands;
using API.Domain.Features.Catalog.Categories;
using API.Domain.Features.Catalog.Products.Enums;
using API.Domain.Features.Catalog.Units;
using API.Domain.Common.Entities;

namespace API.Domain.Features.Catalog.Products
{
    public partial class Product : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public InventoryType InventoryType { get; private set; }

        public long? BrandId { get; private set; }
        public long CategoryId { get; private set; }
        public long UnitId { get; private set; }

        public Brand? Brand { get; private set; }
        public Category Category { get; private set; } = null!;
        public Unit Unit { get; private set; } = null!;
    }

    public partial class Product
    {
        public Product SetBrand(long? brandId)
        {
            BrandId = brandId;
            return this;
        }

        public Product SetCategory(long categoryId)
        {
            CategoryId = categoryId;
            return this;
        }

        public Product SetUnit(long unitId)
        {
            UnitId = unitId;
            return this;
        }
    }
}
