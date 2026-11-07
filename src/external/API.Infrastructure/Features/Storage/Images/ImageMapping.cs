using AutoMapper;
using API.Contract.DTOs.Storage.Images;
using API.Contract.ImportDataRows.Storage;
using API.Domain.Features.Storage.Images;
using API.Domain.Common.Models.Results;

namespace API.Infrastructure.Features.Storage.Images
{
    internal class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<Image, ImageResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CloudinaryUploadResult, Image>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PublicId, opt => opt.Ignore());

            CreateMap<Image, UploadImageResponse>()
                .ForMember(dest => dest.ImagePublicId, opt => opt.MapFrom(src => src.PublicId));

            CreateMap<ImageRow, Image>();
        }
    }
}
