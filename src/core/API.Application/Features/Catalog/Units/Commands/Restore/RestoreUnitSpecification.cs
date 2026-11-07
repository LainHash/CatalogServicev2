using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Units;

namespace API.Application.Features.Catalog.Units.Commands.Restore
{
    public class RestoreUnitSpecification
        : BaseSpecification<Unit>
    {
        public RestoreUnitSpecification(RestoreUnitCommand command)
        {
            Criteria = unit => unit.PublicId == command.Id;
        }
    }
}
