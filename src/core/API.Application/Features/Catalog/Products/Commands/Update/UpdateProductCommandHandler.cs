using MediatR;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Products.Commands.Update
{
    internal class UpdateProductCommandHandler(IProductService productService)
                : IRequestHandler<UpdateProductCommand, Result<ProductResponse>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateProductSpecification(request);
            var response = await _productService.UpdateAsync(specification, request, cancellationToken);
            return response;
        }
    }
}
