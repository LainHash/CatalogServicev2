using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Delete
{
    internal class DeleteCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeleteCategorySpecification(request);
            var response = await _categoryService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}
