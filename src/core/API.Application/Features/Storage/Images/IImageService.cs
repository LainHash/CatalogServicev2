using API.Application.Features.Storage.Images.Commands.UploadBrandImage;
using API.Application.Features.Storage.Images.Commands.UploadProductImage;
using API.Application.Features.Storage.Images.Queries;
using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Storage.Images
{
    public interface IImageService
    {
        Task<PaginationResult<IEnumerable<ImageResponse>>> GetAllAsync(
            GetAllImagesSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageCommand command,
            CancellationToken cancellationToken);

        Task<Result<UploadImageResponse>> UploadBrandImageAsync(
            UploadBrandImageCommand command,
            CancellationToken cancellationToken);
    }
}
