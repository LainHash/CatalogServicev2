using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Update
{
    public record UpdateBrandCommand(Guid Id, UpdateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
