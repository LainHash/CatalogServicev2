using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Delete
{
    public record DeleteUnitCommand(Guid Id)
        : IRequest<Result>
    {
    }
}
