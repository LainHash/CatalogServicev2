using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Restore
{
    internal class RestoreUnitCommandHandler(IUnitService unitService)
                : IRequestHandler<RestoreUnitCommand, Result>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<Result> Handle(RestoreUnitCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreUnitSpecification(request);
            var response = await _unitService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
