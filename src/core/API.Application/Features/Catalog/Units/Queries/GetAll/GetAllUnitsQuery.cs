using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Queries.GetAll
{
    public record GetAllUnitsQuery
        : PaginationQuery, IRequest<PaginationResult<IEnumerable<UnitResponse>>>
    {
    }
}
