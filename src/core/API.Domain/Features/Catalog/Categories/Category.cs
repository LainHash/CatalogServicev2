using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Products;

namespace API.Domain.Features.Catalog.Categories
{
    public partial class Category : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public long? ParentId { get; private set; }
        public Category? Parent { get; private set; }
        public ICollection<Category> Childrens { get; private set; } = [];

        public ICollection<Product> Products { get; private set; } = [];
    }

    public partial class Category
    {
        public Category SetParent(long? parentId)
        {
            ParentId = parentId;
            return this;
        }
    }
}
