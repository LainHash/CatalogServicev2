using API.Contract.DTOs.Catalog.Products;
using API.Domain.Features.Catalog.Products;
using API.Application.Features.Catalog.Products.Commands.Create;
using API.Application.Features.Catalog.Products.Commands.Update;
using API.Application.Features.Catalog.Products.Queries.GetById;
using API.Application.Features.Catalog.Products.Queries.GetAll;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;

namespace API.Application.Features.Catalog.Products
{
    public interface IProductService
    {
        Task<PaginationResult<IEnumerable<ProductResponse>>> GetAllAsync(
            GetAllProductsSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ProductDetailResponse>> GetByIdAsync(
            GetProductByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<ProductResponse>> CreateAsync(
            CreateProductSpecification specification,
            CreateProductCommand command,
            CancellationToken cancellationToken = default);

        Task<Result<ProductResponse>> UpdateAsync(
            UpdateProductSpecification specification,
            UpdateProductCommand command,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken = default);
    }
}
