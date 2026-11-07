using API.Application.Features.Catalog.Brands.Commands.Create;
using API.Application.Features.Catalog.Brands.Commands.Update;
using API.Application.Features.Catalog.Brands.Queries.GetById;
using API.Contract.DTOs.Catalog.Brands;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;
using API.Domain.Features.Catalog.Brands;

namespace API.Application.Features.Catalog.Brands
{
    public interface IBrandService
    {
        Task<PaginationResult<IEnumerable<BrandResponse>>> GetAllAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);

        Task<Result<BrandDetailResponse>> GetByIdAsync(
            GetBrandByIdSpecification specification,
            CancellationToken cancellationToken);

        Task<Result<BrandResponse>> CreateAsync(
            CreateBrandCommand command,
            CancellationToken cancellationToken);

        Task<Result<BrandResponse>> UpdateAsync(
            UpdateBrandCommand command,
            UpdateBrandSpecification specification,
            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);

        Task<Result> RestoreAsync(
            ISpecification<Brand> specification,
            CancellationToken cancellationToken);
    }
}
