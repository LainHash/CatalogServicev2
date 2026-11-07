using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Units;

namespace API.Application.Features.Catalog.Units.Commands.Update
{
    public class UpdateUnitSpecification
        : BaseSpecification<Unit>
    {
        public UpdateUnitSpecification(UpdateUnitCommand command)
        {
            Criteria = u => u.PublicId == command.Id;
        }
    }
}
