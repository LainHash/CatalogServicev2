using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Delete
{
    internal class DeleteBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<DeleteBrandCommand, Result>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteBrandSpecification(request);
            var response = await _brandService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
