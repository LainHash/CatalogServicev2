namespace API.Contract.DTOs.Storage.ProductImages
{
    public class UploadProductImageRequest
    {
        public string? AltText { get; set; }

        public bool IsPrimary { get; set; }
    }
}
