using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Queries
{
    internal class GetAllImagesQueryHandler(IImageService imageService)
                : IRequestHandler<GetAllImagesQuery, PaginationResult<IEnumerable<ImageResponse>>>
    {
        private readonly IImageService _imageService = imageService;

        public async Task<PaginationResult<IEnumerable<ImageResponse>>> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllImagesSpecification(request);
            return await _imageService.GetAllAsync(specification, cancellationToken);
        }
    }
}
