using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Commands.Update
{
    public record UpdateCategoryCommand(Guid Id, UpdateCategoryRequest Body)
        : IRequest<Result<CategoryResponse>>
    {
    }
}
