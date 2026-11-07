using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Products;

namespace API.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductSpecification
        : BaseSpecification<Product>
    {
        public RestoreProductSpecification(RestoreProductCommand command)
        {
            Criteria = p => p.PublicId == command.Id;
        }
    }
}
