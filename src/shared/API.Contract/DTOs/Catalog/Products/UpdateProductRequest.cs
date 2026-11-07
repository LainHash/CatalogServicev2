namespace API.Contract.DTOs.Catalog.Products
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? BrandPublicId { get; set; }
        public Guid CategoryPublicId { get; set; }
        public Guid UnitPublicId { get; set; }
    }
}
