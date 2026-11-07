using AutoMapper;
using API.Contract.DTOs.Storage.ProductImages;
using API.Domain.Features.Storage.ProductImages;
using API.Contract.ImportDataRows.Storage;

namespace API.Infrastructure.Features.Storage.ProductImages
{
    internal class ProductImageMapping : Profile
    {
        public ProductImageMapping()
        {
            CreateMap<ProductImage, ProductImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.PublicId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<ProductImageRow, ProductImage>();
        }
    }
}
