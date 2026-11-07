using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Queries.GetAll
{
    internal class GetAllCategoriesQueryHandler(ICategoryService categoryService)
        : IRequestHandler<GetAllCategoriesQuery, PaginationResult<IEnumerable<CategoryResponse>>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<PaginationResult<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllCategoriesSpecification(request);
            var response = await _categoryService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}
