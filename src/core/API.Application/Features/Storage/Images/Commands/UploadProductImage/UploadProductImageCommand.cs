using API.Contract.DTOs.Storage.Images;
using API.Contract.DTOs.Storage.ProductImages;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Commands.UploadProductImage
{
    public record UploadProductImageCommand(Guid ProductId,
                                            Stream FileStream,
                                            string FileName,
                                            UploadProductImageRequest Metadata)
        : IRequest<Result<UploadImageResponse>>
    {
    }
}
