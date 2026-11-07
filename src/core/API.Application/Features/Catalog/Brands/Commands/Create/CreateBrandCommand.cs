using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Create
{
    public record CreateBrandCommand(CreateBrandRequest Body)
        : IRequest<Result<BrandResponse>>
    {
    }
}
