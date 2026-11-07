using API.Domain.Features.Catalog.Units.Enums;

namespace API.Contract.ImportDataRows.Catalog
{
    public class UnitRow
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;

        public UnitType Type { get; set; }
    }

    public class ChildUnitRow : UnitRow
    {
        public Guid ParentPublicId { get; set; }
    }
}
