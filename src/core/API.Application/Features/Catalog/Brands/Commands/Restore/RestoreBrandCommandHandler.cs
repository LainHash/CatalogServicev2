using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Restore
{
    internal class RestoreBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<RestoreBrandCommand, Result>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result> Handle(RestoreBrandCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreBrandSpecification(request);
            var response = await _brandService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
