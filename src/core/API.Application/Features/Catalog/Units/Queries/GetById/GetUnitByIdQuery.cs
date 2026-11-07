using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Queries.GetById
{
    public record GetUnitByIdQuery(Guid Id)
        : IRequest<Result<UnitDetailResponse>>
    {
    }
}
