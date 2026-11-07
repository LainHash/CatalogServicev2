using Microsoft.EntityFrameworkCore;
using API.Domain.Features.Catalog.Units;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Units.Queries.GetAll
{
    public class GetAllUnitsSpecification
        : BaseSpecification<Unit>
    {
        public GetAllUnitsSpecification(GetAllUnitsQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = u =>
                    EF.Functions.Like(u.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(u.Symbol, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(u => u.Name);
                    else
                        ApplyOrderByDescending(u => u.Name);
                    break;
                case "symbol":
                    if (query.IsAscending)
                        ApplyOrderBy(u => u.Symbol);
                    else
                        ApplyOrderByDescending(u => u.Symbol);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyOrderBy(u => u.CreatedAt);
                    else
                        ApplyOrderByDescending(u => u.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}
