using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Commands.UploadProductImage
{
    internal class UploadProductImageCommandHandler(IImageService imageService)
        : IRequestHandler<UploadProductImageCommand, Result<UploadImageResponse>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<Result<UploadImageResponse>> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _imageService.UploadProductImageAsync(request, cancellationToken);
            return response;
        }
    }
}
