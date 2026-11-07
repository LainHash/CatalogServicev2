using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Units.Enums;

namespace API.Domain.Features.Catalog.Units
{
    public class Unit : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string Symbol { get; private set; } = null!;

        public UnitType Type { get; private set; }
    }
}
