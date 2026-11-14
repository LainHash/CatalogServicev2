using API.Contract.DTOs;
using API.Contract.DTOs.Catalog.Brands;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Common.Entities;
using API.Domain.Features.Catalog.Brands;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Brands
{
    internal class BrandMapping : Profile
    {
        public BrandMapping()
        {
            CreateMap<BrandRow, Brand>();

            CreateMap<Brand, BrandResponse>()
                .ForMember(dest => dest.ProductCount,
                    opt => opt.MapFrom(src => src.Products.Count()))
                .ForMember(dest => dest.PrimaryImage,
                    opt => opt.MapFrom(src => src.BrandImages.FirstOrDefault(x => x.IsPrimary)))
                .IncludeBase<SoftDeletableEntity, SoftDeletableResponse>();

            CreateMap<Brand, BrandDetailResponse>()
                .IncludeBase<Brand, BrandResponse>();

            CreateMap<CreateBrandRequest, Brand>();

            CreateMap<UpdateBrandRequest, Brand>();
        }
    }
}
