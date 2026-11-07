using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Products;

namespace API.Application.Features.Catalog.Products.Commands.Delete
{
    public class DeleteProductSpecification
        : BaseSpecification<Product>
    {
        public DeleteProductSpecification(DeleteProductCommand command)
        {
            Criteria = p => p.PublicId == command.Id;
        }
    }
}
