using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Queries.GetById
{
    internal class GetCategoryByIdQueryHandler(ICategoryService categoryService)
                : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDetailResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryDetailResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCategoryByIdSpecification(request);
            var response = await _categoryService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}
