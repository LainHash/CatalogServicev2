namespace API.Contract.ImportDataRows.Catalog
{
    public class BrandRow
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
