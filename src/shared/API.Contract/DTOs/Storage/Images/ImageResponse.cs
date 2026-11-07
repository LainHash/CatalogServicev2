namespace API.Contract.DTOs.Storage.Images
{
    public class ImageResponse
    {
        public Guid Id { get; set; }
        public string AltText { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
