using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Create
{
    public record CreateUnitCommand(CreateUnitRequest Body)
        : IRequest<Result<UnitResponse>>
    {
    }
}
