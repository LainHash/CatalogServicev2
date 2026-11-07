using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
