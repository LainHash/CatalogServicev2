using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;

namespace API.Application.Features.Catalog.Brands.Commands.Restore
{
    public class RestoreBrandSpecification
        : BaseSpecification<Brand>
    {
        public RestoreBrandSpecification(RestoreBrandCommand command)
        {
            Criteria = brand => brand.PublicId == command.Id;
        }
    }
}
