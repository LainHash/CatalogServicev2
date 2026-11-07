using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Features.Catalog.Brands.Queries.GetById
{
    public class GetBrandByIdSpecification
        : BaseSpecification<Brand>
    {
        public GetBrandByIdSpecification(GetBrandByIdQuery query)
        {
            AddInclude(x => x.Products);

            Criteria = brand => brand.PublicId == query.Id;
        }
    }
}
