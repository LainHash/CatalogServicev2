using API.Domain.Features.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using API.Domain.Features.Catalog.Products.Enums;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsSpecification
        : BaseSpecification<Product>
    {
        public GetAllProductsSpecification(GetAllProductsQuery query)
        {
            EnableSoftDeleteFilter();

            AddCriteria(p => !p.IsDeleted);

            AddInclude(p => p.Category);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);

            //AddIncludeAggregator(x => x.Include(p => p.ProductImages)
            //                            .ThenInclude(pi => pi.Image));

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    (p.Description != null && EF.Functions.Like(p.Description, $"%{query.Keyword}%")) ||
                    EF.Functions.Like(p.Category.Name, $"%{query.Keyword}%") ||
                    (p.Brand != null && EF.Functions.Like(p.Brand.Name, $"%{query.Keyword}%")));
            }

            if (query.CategoryId is not null)
            {
                AddCriteria(p => p.Category.PublicId == query.CategoryId);
            }

            if (query.BrandId is not null)
            {
                AddCriteria(p => p.Brand != null && p.Brand.PublicId == query.BrandId);
            }

            if (!string.IsNullOrEmpty(query.InventoryType))
            {
                if (Enum.TryParse<InventoryType>(query.InventoryType, true, out var invType))
                {
                    AddCriteria(p => p.InventoryType == invType);
                }
            }

            switch (query.SortField?.ToLower())
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
