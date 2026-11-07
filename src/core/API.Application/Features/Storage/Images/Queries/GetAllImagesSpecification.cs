using API.Domain.Common.Specifications;
using API.Domain.Features.Storage.Images;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Features.Storage.Images.Queries
{
    public class GetAllImagesSpecification
        : BaseSpecification<Image>
    {
        public GetAllImagesSpecification(GetAllImagesQuery query)
        {
            ApplyDateRange(query.From, query.To, x => x.CreatedAt);
            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = u =>
                    EF.Functions.Like(u.AltText, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case "created_at":
                    if (query.IsAscending)
                        ApplyOrderBy(u => u.CreatedAt);
                    else
                        ApplyOrderByDescending(u => u.CreatedAt);
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
