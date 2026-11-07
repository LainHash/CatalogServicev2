using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;

namespace API.Application.Features.Catalog.Brands.Commands.Delete
{
    public class DeleteBrandSpecification
        : BaseSpecification<Brand>
    {
        public DeleteBrandSpecification(DeleteBrandCommand command)
        {
            Criteria = brand => brand.PublicId == command.Id;
        }
    }
}
