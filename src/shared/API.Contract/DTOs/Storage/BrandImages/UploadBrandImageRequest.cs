namespace API.Contract.DTOs.Storage.BrandImages
{
    public class UploadBrandImageRequest
    {
        public string? AltText { get; set; }

        public bool IsPrimary { get; set; }
    }
}
