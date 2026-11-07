using API.Domain.Features.Catalog.Categories;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Categories.Queries.GetById
{
    public class GetCategoryByIdSpecification
        : BaseSpecification<Category>
    {
        public GetCategoryByIdSpecification(GetCategoryByIdQuery query)
        {
            AddInclude(x => x.Products);

            AddCriteria(category => category.PublicId == query.Id);
        }
    }
}
