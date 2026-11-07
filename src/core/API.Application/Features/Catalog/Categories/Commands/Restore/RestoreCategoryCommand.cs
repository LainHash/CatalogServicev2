using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Restore
{
    public record RestoreCategoryCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
