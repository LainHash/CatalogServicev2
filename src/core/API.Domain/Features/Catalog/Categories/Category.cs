using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Products;

namespace API.Domain.Features.Catalog.Categories
{
    public class Category : SoftDeletableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        public ICollection<Product> Products { get; private set; } = [];
    }
}
