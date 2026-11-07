using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;

namespace API.Application.Features.Catalog.Brands.Commands.Update
{
    public class UpdateBrandSpecification
        : BaseSpecification<Brand>
    {
        public UpdateBrandSpecification(UpdateBrandCommand command)
        {
            Criteria = b => b.PublicId == command.Id;
        }
    }
}
