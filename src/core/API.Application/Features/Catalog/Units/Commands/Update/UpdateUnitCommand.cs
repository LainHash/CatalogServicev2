using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Commands.Update
{
    public record UpdateUnitCommand(Guid Id, UpdateUnitRequest Body)
        : IRequest<Result<UnitResponse>>
    {
    }
}
