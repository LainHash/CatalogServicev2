namespace API.Contract.ImportDataRows.Catalog
{
    public class CategoryRow
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
