using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Queries.GetById
{
    internal class GetBrandByIdQueryHandler(IBrandService brandService)
                : IRequestHandler<GetBrandByIdQuery, Result<BrandDetailResponse>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<Result<BrandDetailResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBrandByIdSpecification(request);
            var response = await _brandService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
