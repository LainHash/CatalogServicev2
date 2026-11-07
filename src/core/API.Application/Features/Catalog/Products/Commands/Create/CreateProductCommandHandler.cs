using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Commands.Create
{
    internal class CreateProductCommandHandler(IProductService productService)
                : IRequestHandler<CreateProductCommand, Result<ProductResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateProductSpecification(request);
            var response = await _productService.CreateAsync(specification, request, cancellationToken);
            return response;
        }
    }
}
