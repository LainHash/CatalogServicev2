using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Units.Commands.Delete
{
    internal class DeleteUnitCommandHandler(IUnitService unitService)
                : IRequestHandler<DeleteUnitCommand, Result>
    {
        private readonly IUnitService _unitService = unitService;

        public async Task<Result> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteUnitSpecification(request);
            var response = await _unitService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
