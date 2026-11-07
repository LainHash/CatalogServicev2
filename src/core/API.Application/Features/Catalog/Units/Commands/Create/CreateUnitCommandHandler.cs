using API.Contract.DTOs.Catalog.Units;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Create
{
    internal class CreateUnitCommandHandler(IUnitService unitService)
                : IRequestHandler<CreateUnitCommand, Result<UnitResponse>>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<Result<UnitResponse>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var response = await _unitService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
