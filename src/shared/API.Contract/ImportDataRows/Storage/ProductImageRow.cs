namespace API.Contract.ImportDataRows.Storage
{
    public class ProductImageRow
    {
        public Guid ProductPublicId { get; set; }
        public Guid ImagePublicId { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}
