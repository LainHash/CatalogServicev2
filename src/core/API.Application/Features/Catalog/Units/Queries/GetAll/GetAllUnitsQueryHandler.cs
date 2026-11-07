using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Queries.GetAll
{
    internal class GetAllUnitsQueryHandler(IUnitService unitService)
        : IRequestHandler<GetAllUnitsQuery, PaginationResult<IEnumerable<UnitResponse>>>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<PaginationResult<IEnumerable<UnitResponse>>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllUnitsSpecification(request);
            var response = await _unitService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
