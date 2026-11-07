using API.Domain.Features.Catalog.Units.Enums;

namespace API.Contract.DTOs.Catalog.Units
{
    public class UnitResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;

        public UnitType Type { get; set; }
    }
}
