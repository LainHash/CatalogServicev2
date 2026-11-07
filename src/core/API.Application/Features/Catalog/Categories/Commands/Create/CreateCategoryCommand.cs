using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Create
{
    public record CreateCategoryCommand(CreateCategoryRequest Body)
        : IRequest<Result<CategoryResponse>>
    {
    }
}
