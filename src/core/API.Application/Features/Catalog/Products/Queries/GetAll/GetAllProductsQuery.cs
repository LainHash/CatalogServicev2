using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Queries.GetAll
{
    public record GetAllProductsQuery(
        Guid? CategoryId = null,
        Guid? BrandId = null,
        string? InventoryType = null)
        : PaginationQuery, IRequest<PaginationResult<IEnumerable<ProductResponse>>>
    {
    }
}
