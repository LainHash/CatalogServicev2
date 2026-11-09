using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Queries.GetAll
{
    internal class GetAllProductsQueryHandler(IProductService productService)
                : IRequestHandler<GetAllProductsQuery, PaginationResult<IEnumerable<ProductResponse>>>
    {
        private readonly IProductService _productService = productService;

        public async Task<PaginationResult<IEnumerable<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllProductsSpecification(request);
            var response = await _productService.GetAllAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
