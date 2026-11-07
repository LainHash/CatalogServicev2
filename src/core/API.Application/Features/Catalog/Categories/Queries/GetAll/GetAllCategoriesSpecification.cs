using Microsoft.EntityFrameworkCore;
using API.Domain.Features.Catalog.Categories;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Categories.Queries.GetAll
{
    public class GetAllCategoriesSpecification
        : BaseSpecification<Category>
    {
        public GetAllCategoriesSpecification(GetAllCategoriesQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
