using API.Domain.Features.Catalog.Categories;
using API.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Features.Catalog.Categories.Queries.GetById
{
    public class GetCategoryByIdSpecification
        : BaseSpecification<Category>
    {
        public GetCategoryByIdSpecification(GetCategoryByIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Products);

            AddIncludeAggregator(x => x.Include(c => c.Childrens)
                                       .ThenInclude(ch => ch.Products));

            AddCriteria(category => category.PublicId == query.Id);
        }
    }
}
