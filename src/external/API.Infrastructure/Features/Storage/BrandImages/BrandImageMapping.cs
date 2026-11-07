using AutoMapper;
using API.Contract.DTOs.Storage.BrandImages;
using API.Contract.DTOs.Storage.Images;
using API.Domain.Features.Storage;
using API.Contract.ImportDataRows.Storage;
using API.Domain.Features.Storage.BrandImages;

namespace API.Infrastructure.Features.Storage.BrandImages
{
    internal class BrandImageMapping : Profile
    {
        public BrandImageMapping()
        {
            CreateMap<BrandImage, BrandImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.PublicId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<BrandImageRow, BrandImage>();
        }
    }
}
