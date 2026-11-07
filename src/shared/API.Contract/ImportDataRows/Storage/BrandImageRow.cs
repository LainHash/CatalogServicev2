namespace API.Contract.ImportDataRows.Storage
{
    public class BrandImageRow
    {
        public Guid BrandPublicId { get; set; }
        public Guid ImagePublicId { get; set; }
        public bool IsPrimary { get; set; }
        public int DisplayOrder { get; set; }
    }
}
