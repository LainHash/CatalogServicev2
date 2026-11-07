using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Products;

namespace API.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductSpecification
        : BaseSpecification<Product>
    {
        public UpdateProductSpecification(UpdateProductCommand command)
        {
            Criteria = p => p.PublicId == command.Id;

            AddInclude(p => p.Category);
            AddInclude(p => p.Brand!);
        }
    }
}
