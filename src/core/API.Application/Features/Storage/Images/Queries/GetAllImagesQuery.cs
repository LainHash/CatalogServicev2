using API.Contract.DTOs.Storage.Images;
using API.Domain.Common.Models;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Storage.Images.Queries
{
    public record GetAllImagesQuery
        : PaginationQuery, IRequest<PaginationResult<IEnumerable<ImageResponse>>>
    {
    }
}
