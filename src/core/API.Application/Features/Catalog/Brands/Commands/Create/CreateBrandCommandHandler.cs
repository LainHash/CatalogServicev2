using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Commands.Create
{
    internal class CreateBrandCommandHandler(IBrandService brandService)
                : IRequestHandler<CreateBrandCommand, Result<BrandResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandResponse>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var response = await _brandService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
