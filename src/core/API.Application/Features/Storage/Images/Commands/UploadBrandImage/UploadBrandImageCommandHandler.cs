using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Commands.UploadBrandImage
{
    internal class UploadBrandImageCommandHandler(IImageService imageService)
        : IRequestHandler<UploadBrandImageCommand, Result<UploadImageResponse>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<Result<UploadImageResponse>> Handle(UploadBrandImageCommand request, CancellationToken cancellationToken)
        {
            var response = await _imageService.UploadBrandImageAsync(request, cancellationToken);
            return response;
        }
    }
}
