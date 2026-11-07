using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Queries.GetById
{
    internal class GetProductByIdQueryHandler(IProductService productService)
        : IRequestHandler<GetProductByIdQuery, Result<ProductDetailResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductDetailResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetProductByIdSpecification(request);
            var response = await _productService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
