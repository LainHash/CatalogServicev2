using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Queries.GetAll
{
    public record GetAllBrandsQuery
        : PaginationQuery, IRequest<PaginationResult<IEnumerable<BrandResponse>>>
    {
    }
}
