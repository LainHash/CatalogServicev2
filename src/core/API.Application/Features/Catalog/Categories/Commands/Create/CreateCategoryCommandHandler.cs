using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Create
{
    internal class CreateCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<CreateCategoryCommand, Result<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}
