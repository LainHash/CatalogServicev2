using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Queries.GetById
{
    public record GetBrandByIdQuery(Guid Id)
        : IRequest<Result<BrandDetailResponse>>
    {
    }
}
