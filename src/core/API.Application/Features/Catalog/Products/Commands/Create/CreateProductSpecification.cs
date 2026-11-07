using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Products;

namespace API.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductSpecification
        : BaseSpecification<Product>
    {
        public CreateProductSpecification(CreateProductCommand command)
        {
            AddInclude(p => p.Category);
            AddInclude(p => p.Brand!);
        }

        public void ApplyCriteria(long id)
        {
            Criteria = p => p.Id == id;
        }
    }
}
