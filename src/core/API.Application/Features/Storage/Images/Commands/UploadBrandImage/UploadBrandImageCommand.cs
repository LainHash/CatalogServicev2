using API.Contract.DTOs.Storage.BrandImages;
using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Commands.UploadBrandImage
{
    public record UploadBrandImageCommand(Guid BrandId,
                                          Stream FileStream,
                                          string FileName,
                                          UploadBrandImageRequest Metadata)
        : IRequest<Result<UploadImageResponse>>
    {
    }
}
