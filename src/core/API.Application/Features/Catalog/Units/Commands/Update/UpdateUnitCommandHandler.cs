using MediatR;
using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Units.Commands.Update
{
    internal class UpdateUnitCommandHandler(IUnitService unitService)
                : IRequestHandler<UpdateUnitCommand, Result<UnitResponse>>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<Result<UnitResponse>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateUnitSpecification(request);
            var response = await _unitService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
