namespace API.Contract.DTOs.Storage.Images
{
    public class ImageResponse : AuditableResponse
    {
        public string AltText { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
