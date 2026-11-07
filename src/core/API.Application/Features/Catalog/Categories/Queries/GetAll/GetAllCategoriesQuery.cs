using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Queries.GetAll
{
    public record GetAllCategoriesQuery
        : PaginationQuery, IRequest<PaginationResult<IEnumerable<CategoryResponse>>>
    {
    }
}
