using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Units.Enums;

namespace API.Domain.Features.Catalog.Units
{
    public partial class Unit : SoftDeletableEntity
    {
        public string Name { get; private set; } = null!;
        public string Symbol { get; private set; } = null!;

        public UnitType Type { get; private set; }

        public decimal ConversionRate { get; private set; }

        public long? ParentId { get; private set; }
        public Unit? Parent { get; private set; }
        public ICollection<Unit> Childrens { get; private set; } = [];
    }

    public partial class Unit
    {
        public Unit SetParent(long? parentId)
        {
            ParentId = parentId;
            return this;
        }
    }
}
