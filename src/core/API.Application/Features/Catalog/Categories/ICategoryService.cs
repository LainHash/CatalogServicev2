using API.Application.Features.Catalog.Categories.Commands.Create;
using API.Contract.DTOs.Catalog.Categories;
using API.Application.Features.Catalog.Categories.Commands.Update;
using API.Application.Features.Catalog.Categories.Queries.GetById;
using API.Domain.Features.Catalog.Categories;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<PaginationResult<IEnumerable<CategoryResponse>>> GetAllAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);

        Task<Result<CategoryDetailResponse>> GetByIdAsync(
            GetCategoryByIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<CategoryResponse>> CreateAsync(
            CreateCategoryCommand command,
            CancellationToken cancellationToken);

        Task<Result<CategoryResponse>> UpdateAsync(
            UpdateCategoryCommand command,
            UpdateCategorySpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken);
    }
}
