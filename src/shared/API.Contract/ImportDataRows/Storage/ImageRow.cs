namespace API.Contract.ImportDataRows.Storage
{
    public class ImageRow
    {
        public Guid PublicId { get; set; }
        public string AltText { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string StoragePath { get; set; } = null!;
    }
}
