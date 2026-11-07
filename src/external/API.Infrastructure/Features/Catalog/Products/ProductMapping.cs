using API.Contract.DTOs.Catalog.Products;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Products;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductRow, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.BrandName, 
                    opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null))
                .ForMember(dest => dest.CategoryName, 
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.UnitSymbol, 
                    opt => opt.MapFrom(src => src.Unit != null ? src.Unit.Symbol : string.Empty))
                .ForMember(dest => dest.PrimaryImage,
                    opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(x => x.IsPrimary)));


            CreateMap<Product, ProductDetailResponse>()
                .IncludeBase<Product, ProductResponse>();

            CreateMap<Product, ProductMinimalResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId));

            CreateMap<CreateProductRequest, Product>();

            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
