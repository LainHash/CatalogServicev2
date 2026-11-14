namespace API.Contract.DTOs.Catalog.Categories
{
    public class CategoryResponse : SoftDeletableResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProductCount { get; set; }
    }
}
