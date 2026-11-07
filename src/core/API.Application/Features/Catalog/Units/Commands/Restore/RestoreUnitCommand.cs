using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Restore
{
    public record RestoreUnitCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
