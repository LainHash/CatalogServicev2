using MediatR;
using API.Contract.DTOs.Catalog.Categories;
using API.Domain.Common.Models.Results;

namespace API.Application.Features.Catalog.Categories.Commands.Update
{
    internal class UpdateCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<UpdateCategoryCommand, Result<CategoryResponse>>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result<CategoryResponse>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateCategorySpecification(request);
            var response = await _categoryService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}
