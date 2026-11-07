using API.Application.Features.Storage.Images.Queries;
using API.Contract.DTOs.Storage.Images;
using API.Domain.Features.Catalog.Brands;
using API.Domain.Features.Catalog.Products;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;
using API.Application.Features.Storage.Images;
using API.Domain.Features.Storage.Images;
using API.Domain.Features.Storage.ProductImages;
using API.Domain.Features.Storage.BrandImages;
using API.Application.Features.Business;
using API.Domain.Common.Models.Results;
using API.Domain.Common.Models.Messages;
using API.Application.Features.Storage.Images.Commands.UploadProductImage;
using API.Application.Features.Storage.Images.Commands.UploadBrandImage;

namespace API.Infrastructure.Features.Storage.Images
{
    internal class ImageService : IImageService
    {
        private const int MaxImagesPerProduct = 5;
        private const int MaxImagesPerBrand = 5;

        private readonly IImageRepository _imageRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBrandImageRepository _brandImageRepository;
        private readonly IBrandRepository _brandRepository;

        private readonly ICloudinaryService _cloudinaryService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ImageService> _logger;

        public ImageService(
            IImageRepository imageRepository,
            IProductImageRepository productImageRepository,
            IProductRepository productRepository,
            IBrandImageRepository brandImageRepository,
            IBrandRepository brandRepository,
            ICloudinaryService cloudinaryService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ImageService> logger)
        {
            _imageRepository = imageRepository;
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
            _brandImageRepository = brandImageRepository;
            _brandRepository = brandRepository;
            _cloudinaryService = cloudinaryService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginationResult<IEnumerable<ImageResponse>>> GetAllAsync(
            GetAllImagesSpecification specification,
            CancellationToken cancellationToken)
        {
            var images = await _imageRepository.ToListAsync(specification, cancellationToken);

            var totalItems = await _imageRepository.CountAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<ImageResponse>>(images);
            return PaginationResult<IEnumerable<ImageResponse>>
                .Succeed(response, Success.Retrieved("Image"), totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<UploadImageResponse>> UploadProductImageAsync(
            UploadProductImageCommand command,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository
                .FindByIdAsync(command.ProductId, cancellationToken);
            if (product is null)
            {
                return Result<UploadImageResponse>
                    .Fail(Error.NotFound("Product"), HttpStatusCode.NotFound);
            }

            var currentCount = await _productImageRepository
                .CountByProductIdAsync(product.Id, cancellationToken);
            if (currentCount >= MaxImagesPerProduct)
            {
                return Result<UploadImageResponse>
                    .Fail($"Product đã đạt giới hạn tối đa {MaxImagesPerProduct} ảnh.", HttpStatusCode.UnprocessableEntity);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var baseImageResult = await UploadAndSaveBaseImageAsync(
                    command.FileStream,
                    command.FileName,
                    command.Metadata.AltText,
                    folder: "Foods",
                    cancellationToken);

                if (!baseImageResult.IsSucceed)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result<UploadImageResponse>
                        .Fail(baseImageResult.Message, (HttpStatusCode)baseImageResult.StatusCode);
                }

                if (command.Metadata.IsPrimary)
                {
                    await _productImageRepository
                        .UnsetPrimaryAsync(product.Id, cancellationToken);
                }

                var image = baseImageResult.Data!;

                var productImage = new ProductImage(command.Metadata.IsPrimary, currentCount + 1)
                    .SetProduct(product.Id)
                    .SetImage(image.Id);

                _productImageRepository.Add(productImage);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var response = _mapper.Map<UploadImageResponse>(image);

                return Result<UploadImageResponse>
                    .Succeed(response, "Upload ảnh thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Lỗi khi upload ảnh cho Sản phẩm.");
                return Result<UploadImageResponse>
                    .Fail("Lỗi khi upload ảnh.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<UploadImageResponse>> UploadBrandImageAsync(
            UploadBrandImageCommand command,
            CancellationToken cancellationToken)
        {
            var brand = await _brandRepository
                .FindByIdAsync(command.BrandId, cancellationToken);
            if (brand is null)
            {
                return Result<UploadImageResponse>
                    .Fail(Error.NotFound("Brand"), HttpStatusCode.NotFound);
            }

            var currentCount = await _brandImageRepository
                .CountByBrandIdAsync(brand.Id, cancellationToken);
            if (currentCount >= MaxImagesPerBrand)
            {
                return Result<UploadImageResponse>
                    .Fail($"Brand đã đạt giới hạn tối đa {MaxImagesPerBrand} ảnh.", HttpStatusCode.UnprocessableEntity);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var baseImageResult = await UploadAndSaveBaseImageAsync(
                    command.FileStream,
                    command.FileName,
                    command.Metadata.AltText,
                    folder: "Brands",
                    cancellationToken);

                if (!baseImageResult.IsSucceed)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result<UploadImageResponse>
                        .Fail(baseImageResult.Message, (HttpStatusCode)baseImageResult.StatusCode);
                }

                if (command.Metadata.IsPrimary)
                {
                    await _brandImageRepository
                        .UnsetPrimaryAsync(brand.Id, cancellationToken);
                }

                var image = baseImageResult.Data!;

                var brandImage = new BrandImage(command.Metadata.IsPrimary, currentCount + 1)
                    .SetBrand(brand.Id)
                    .SetImage(image.Id);

                _brandImageRepository.Add(brandImage);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var response = _mapper.Map<UploadImageResponse>(image);

                return Result<UploadImageResponse>
                    .Succeed(response, "Upload ảnh thành công.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Lỗi khi upload ảnh cho Thương hiệu.");
                return Result<UploadImageResponse>
                    .Fail("Lỗi khi upload ảnh.", HttpStatusCode.InternalServerError);
            }
        }

        private async Task<Result<Image>> UploadAndSaveBaseImageAsync(
            Stream fileStream,
            string fileName,
            string? altText,
            string folder,
            CancellationToken cancellationToken)
        {
            var uploadResult = await _cloudinaryService
                .UploadAsync(
                    fileStream: fileStream,
                    fileName: fileName,
                    folder: folder,
                    cancellationToken: cancellationToken);

            if (!uploadResult.IsSuccess)
            {
                return Result<Image>
                    .Fail(uploadResult.ErrorMessage ?? "Upload ảnh thất bại.", HttpStatusCode.BadGateway);
            }

            var image = _mapper.Map<Image>(uploadResult)
                .SetAltText(altText ?? fileName);

            _imageRepository.Add(image);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Image>
                .Succeed(image, "Upload image successfully.");
        }
    }
}
