using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Units;

namespace API.Application.Features.Catalog.Units.Queries.GetById
{
    public class GetUnitByIdSpecification
        : BaseSpecification<Unit>
    {
        public GetUnitByIdSpecification(GetUnitByIdQuery query)
        {
            Criteria = unit => unit.PublicId == query.Id;
        }
    }
}
