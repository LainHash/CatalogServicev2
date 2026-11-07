using API.Domain.Common.Models.Results;

namespace API.Application.Features.Business
{
    public interface ICloudinaryService
    {
        Task<CloudinaryUploadResult> UploadAsync(
            Stream fileStream,
            string fileName,
            string? folder = null,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            string publicId,
            CancellationToken cancellationToken = default);
    }
}
