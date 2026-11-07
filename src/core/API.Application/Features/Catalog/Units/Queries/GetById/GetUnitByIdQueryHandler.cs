using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Queries.GetById
{
    internal class GetUnitByIdQueryHandler(IUnitService unitService)
                : IRequestHandler<GetUnitByIdQuery, Result<UnitDetailResponse>>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<Result<UnitDetailResponse>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetUnitByIdSpecification(request);
            var response = await _unitService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
