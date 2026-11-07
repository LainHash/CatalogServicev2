using API.Contract.DTOs.Catalog.Categories;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Categories;
using AutoMapper;

namespace API.Infrastructure.Features.Catalog.Categories
{
    internal class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryRow, Category>();

            CreateMap<ChildCategoryRow, Category>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id, 
                    opt => opt.MapFrom(src => src.PublicId))
                .ForMember(dest => dest.ProductCount,
                    opt => opt.MapFrom(src => src.Products.Count() + src.Childrens.Sum(x => x.Products.Count)));

            CreateMap<Category, CategoryDetailResponse>()
                .IncludeBase<Category, CategoryResponse>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                    src.Products.Concat(src.Childrens.SelectMany(x => x.Products))));

            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
