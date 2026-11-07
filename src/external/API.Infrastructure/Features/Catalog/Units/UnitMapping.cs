using API.Contract.DTOs.Catalog.Units;
using API.Domain.Features.Catalog.Units;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Units
{
    internal class UnitMapping : Profile
    {
        public UnitMapping()
        {
            //CreateMap<UnitRow, Unit>();

            //CreateMap<ChildUnitRow, Unit>();

            CreateMap<Unit, UnitResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId));

            CreateMap<Unit, UnitDetailResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateUnitRequest, Unit>();

            CreateMap<UpdateUnitRequest, Unit>();
        }
    }
}
