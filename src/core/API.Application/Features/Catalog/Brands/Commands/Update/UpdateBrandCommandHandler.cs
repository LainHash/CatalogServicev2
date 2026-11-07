using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Update
{
    internal class UpdateBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<UpdateBrandCommand, Result<BrandResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandResponse>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateBrandSpecification(request);
            var response = await _brandService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
