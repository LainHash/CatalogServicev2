using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Products;
using Microsoft.EntityFrameworkCore;

namespace API.Application.Features.Catalog.Products.Queries.GetById
{
    public class GetProductByIdSpecification
        : BaseSpecification<Product>
    {
        public GetProductByIdSpecification(GetProductByIdQuery query)
        {
            EnableSoftDeleteFilter();

            Criteria = p => p.PublicId == query.Id;

            AddInclude(p => p.Category);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);

            //AddIncludeAggregator(x => x.Include(p => p.ProductImages)
            //                            .ThenInclude(pi => pi.Image));
        }
    }
}
