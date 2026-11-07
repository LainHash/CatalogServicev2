using API.Domain.Features.Catalog.Products.Enums;

namespace API.Contract.ImportDataRows.Catalog
{
    public class ProductRow
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public InventoryType InventoryType { get; set; }

        public Guid? BrandPublicId { get; set; }
        public Guid CategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
