using API.Application.Features.Catalog.Products.Commands.Create;
using API.Application.Features.Catalog.Products.Commands.Update;
using API.Application.Features.Catalog.Products.Queries.GetAll;
using API.Application.Features.Catalog.Products.Queries.GetById;
using API.Contract.DTOs.Catalog.Products;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Catalog.Brands;
using AutoMapper;
using System.Net;
using API.Domain.Features.Catalog.Units;
using API.Domain.Features.Catalog.Products.Enums;
using API.Application.Features.Catalog.Products;
using API.Domain.Features.Catalog.Categories;
using API.Application.Features.Business;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Models.Messages;
using API.Domain.Common.Specifications;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
        }

        public async Task<PaginationResult<IEnumerable<ProductResponse>>> GetAllAsync(
            GetAllProductsSpecification specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _productRepository
                .CountAsync(specification, cancellationToken);

            var products = await _productRepository
                .ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return PaginationResult<IEnumerable<ProductResponse>>
                .Succeed(response, Success.Retrieved("Products"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<ProductDetailResponse>> GetByIdAsync(
            GetProductByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .FindAsync(specification, cancellationToken);
            if (product is null)
            {
                return Result<ProductDetailResponse>
                    .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<ProductDetailResponse>(product);

            return Result<ProductDetailResponse>
                .Succeed(response, Success.Retrieved("Product"));
        }

        public async Task<Result<ProductResponse>> CreateAsync(
            CreateProductSpecification specification,
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository
                .FindByIdAsync(command.Body.CategoryPublicId, cancellationToken);
            if(category is null)
            {
                return Result<ProductResponse>
                    .Fail(Error.NotFound("Category"), HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (command.Body.BrandPublicId is not null)
            {
                brand = await _brandRepository
                    .FindByIdAsync(command.Body.BrandPublicId.Value, cancellationToken);

                if (brand is null)
                {
                    return Result<ProductResponse>
                        .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository
                .FindByIdAsync(command.Body.UnitPublicId, cancellationToken);
            if (unit is null)
            {
                return Result<ProductResponse>
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            var product = _mapper.Map<Product>(command.Body)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);

            _productRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(product.Id);
            var createdProduct = await _productRepository
                .FindAsync(specification, cancellationToken);
            
            var response = _mapper.Map<ProductResponse>(createdProduct);
            return Result<ProductResponse>
                .Succeed(response, Success.Created("Product"), HttpStatusCode.Created);
        }

        public async Task<Result<ProductResponse>> UpdateAsync(
            UpdateProductSpecification specification,
            UpdateProductCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository
                .FindByIdAsync(command.Body.CategoryPublicId, cancellationToken);
            if (category is null)
            {
                return Result<ProductResponse>
                    .Fail(Error.NotFound("Category"), HttpStatusCode.NotFound);
            }

            Brand? brand = null;
            if (command.Body.BrandPublicId is not null)
            {
                brand = await _brandRepository
                    .FindByIdAsync(command.Body.BrandPublicId.Value, cancellationToken);

                if (brand is null)
                {
                    return Result<ProductResponse>
                        .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
                }
            }

            var unit = await _unitRepository
                .FindByIdAsync(command.Body.UnitPublicId, cancellationToken);
            if (unit is null)
            {
                return Result<ProductResponse>
                    .Fail(Error.NotFound("Unit"), HttpStatusCode.NotFound);
            }

            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if(product is null)
            {
                return Result<ProductResponse>
                    .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, product)
                .SetCategory(category.Id)
                .SetBrand(brand?.Id)
                .SetUnit(unit.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<ProductResponse>(product);
            return Result<ProductResponse>
                .Succeed(response, Success.Updated("Product"));
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if (product is null)
            {
                return Result
                    .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
            }

            if (product.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Product"), HttpStatusCode.BadRequest);
            }

            product.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Product"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Product> specification,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(specification, cancellationToken);
            if (product is null)
            {
                return Result
                    .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
            }

            if (!product.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Product"), HttpStatusCode.BadRequest);
            }

            product.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Product"));
        }
    }
}
