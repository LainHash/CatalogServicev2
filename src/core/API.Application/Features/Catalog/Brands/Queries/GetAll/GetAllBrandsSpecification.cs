using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Features.Catalog.Brands.Queries.GetAll
{
    public class GetAllBrandsSpecification
        : BaseSpecification<Brand>
    {
        public GetAllBrandsSpecification(GetAllBrandsQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(x => x.Products);

            AddIncludeAggregator(x => x
                .Include(b => b.BrandImages)
                    .ThenInclude(bi => bi.Image));

            ApplyDateRange(query.From, query.To, p => p.CreatedAt);

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%"));
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
