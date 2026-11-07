using API.Domain.Common.Models.Results;
using MediatR;

namespace API.Application.Features.Catalog.Categories.Commands.Restore
{
    internal class RestoreCategoryCommandHandler(ICategoryService categoryService)
                : IRequestHandler<RestoreCategoryCommand, Result>
    {
        private readonly ICategoryService _categoryService = categoryService;

        public async Task<Result> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreCategorySpecification(request);
            var response = await _categoryService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}
