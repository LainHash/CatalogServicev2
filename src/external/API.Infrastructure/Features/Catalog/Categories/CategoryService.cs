using AutoMapper;
using API.Application.Features.Catalog.Categories.Commands.Create;
using API.Application.Features.Catalog.Categories.Commands.Update;
using API.Application.Features.Catalog.Categories.Queries.GetById;
using API.Contract.DTOs.Catalog.Categories;
using System.Net;
using API.Application.Features.Catalog.Categories;
using API.Domain.Features.Catalog.Categories;
using API.Application.Features.Business;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Specifications;
using API.Domain.Common.Models.Messages;

namespace API.Infrastructure.Features.Catalog.Categories
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<IEnumerable<CategoryResponse>>> GetAllAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var totalItems = await _categoryRepository.CountAsync(specification, cancellationToken);

            var categories = await _categoryRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            return PaginationResult<IEnumerable<CategoryResponse>>
                .Succeed(response, Success.Retrieved("Product Category"), totalItems, specification.Skip, specification.Take);

        }

        public async Task<Result<CategoryDetailResponse>> GetByIdAsync(
            GetCategoryByIdSpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result<CategoryDetailResponse>
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<CategoryDetailResponse>(category);
            return Result<CategoryDetailResponse>
                .Succeed(response, Success.Retrieved("Product Category"));
        }

        public async Task<Result<CategoryResponse>> CreateAsync(
            CreateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if(category is not null)
            {
                return Result<CategoryResponse>
                    .Fail(Error.ExistedName("Product Category"), HttpStatusCode.Conflict);
            }

            category = _mapper.Map<Category>(command.Body);
            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success.Created("Product Category"), HttpStatusCode.Created);
        }

        public async Task<Result<CategoryResponse>> UpdateAsync(
            UpdateCategoryCommand command,
            UpdateCategorySpecification specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category is null)
            {
                return Result<CategoryResponse>
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(await _categoryRepository
                .IsExistingNameAsync(command.Id, command.Body.Name, cancellationToken))
            {
                return Result<CategoryResponse>
                    .Fail(Error.ExistedName("Product Category"), HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CategoryResponse>(category);
            return Result<CategoryResponse>
                .Succeed(response, Success.Updated("Product Category"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(category.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Product Category"), HttpStatusCode.BadRequest);
            }

            category.SoftDelete();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Product Category"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Category> specification,
            CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindAsync(specification, cancellationToken);
            if (category == null)
            {
                return Result
                    .Fail(Error.NotFound("Product Category"), HttpStatusCode.NotFound);
            }

            if(!category.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Product Category"), HttpStatusCode.BadRequest);
            }

            category.Restore();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Product Category"));
        }
    }
}
