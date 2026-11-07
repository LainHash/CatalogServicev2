namespace API.Contract.DTOs.Storage.Images
{
    public class UploadImageResponse
    {
        public Guid ImagePublicId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
    }
}
