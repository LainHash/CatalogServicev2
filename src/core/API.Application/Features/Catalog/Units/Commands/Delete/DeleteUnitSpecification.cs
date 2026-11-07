using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Units;

namespace API.Application.Features.Catalog.Units.Commands.Delete
{
    public class DeleteUnitSpecification
        : BaseSpecification<Unit>
    {
        public DeleteUnitSpecification(DeleteUnitCommand command)
        {
            Criteria = unit => unit.PublicId == command.Id;
        }
    }
}
