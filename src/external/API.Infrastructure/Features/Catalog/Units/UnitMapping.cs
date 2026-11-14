using API.Contract.DTOs;
using API.Contract.DTOs.Catalog.Units;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Units;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Units
{
    internal class UnitMapping : Profile
    {
        public UnitMapping()
        {
            CreateMap<UnitRow, Unit>();

            CreateMap<ChildUnitRow, Unit>();

            CreateMap<Unit, UnitResponse>()
                .IncludeBase<SoftDeletableEntity, SoftDeletableResponse>();

            CreateMap<Unit, UnitDetailResponse>()
                .IncludeBase<Unit, UnitResponse>();

            CreateMap<CreateUnitRequest, Unit>();

            CreateMap<UpdateUnitRequest, Unit>();
        }
    }
}
