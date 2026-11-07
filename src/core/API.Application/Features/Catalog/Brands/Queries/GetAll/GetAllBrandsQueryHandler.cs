using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Brands.Queries.GetAll
{
    internal class GetAllBrandsQueryHandler(IBrandService brandService)
        : IRequestHandler<GetAllBrandsQuery, PaginationResult<IEnumerable<BrandResponse>>>
    {
        private readonly IBrandService _brandService = brandService;

        public async Task<PaginationResult<IEnumerable<BrandResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllBrandsSpecification(request);
            var response = await _brandService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
