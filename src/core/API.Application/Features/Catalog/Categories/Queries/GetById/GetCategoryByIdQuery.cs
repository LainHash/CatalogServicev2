using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Queries.GetById
{
    public record GetCategoryByIdQuery(Guid Id) 
        : IRequest<Result<CategoryDetailResponse>>
    {
    }
}
