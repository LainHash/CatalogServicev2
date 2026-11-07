using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Features.Catalog.Brands;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Brands
{
    internal class BrandMapping : Profile
    {
        public BrandMapping()
        {
            //CreateMap<BrandRow, Brand>();

            CreateMap<Brand, BrandResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId));

            CreateMap<Brand, BrandDetailResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.Products,
                    opt => opt.MapFrom(src => src.Products));

            CreateMap<CreateBrandRequest, Brand>();

            CreateMap<UpdateBrandRequest, Brand>();
        }
    }
}
