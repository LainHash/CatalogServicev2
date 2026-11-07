using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Storage;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Storage.Images;
using API.Domain.Features.Storage.ProductImages;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Storage
{
    internal class ProductImageSheetImporter
        : BaseSheetImporter<ProductImageRow>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        private readonly Dictionary<Guid, Product> _productCache = [];
        private readonly Dictionary<Guid, Image> _imageCache = [];

        public ProductImageSheetImporter(
            IProductImageRepository productImageRepository,
            IProductRepository productRepository,
            IImageRepository imageRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public override string SheetName => "ProductImages";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<ProductImageRow> rows,
            CancellationToken cancellationToken)
        {
            _productCache.Clear();
            _imageCache.Clear();

            var products = await _productRepository.ToListAsync(cancellationToken);
            foreach (var product in products)
            {
                _productCache[product.PublicId] = product;
            }

            var images = await _imageRepository.ToListAsync(cancellationToken);
            foreach (var image in images)
            {
                _imageCache[image.PublicId] = image;
            }
        }

        protected async override Task ProcessRowAsync(
            ProductImageRow row,
            int rowIndex,
            ImportResultResponse result,
            CancellationToken cancellationToken)
        {
            var productPublicId = row.ProductPublicId;
            if (!_productCache.TryGetValue(productPublicId, out var product))
            {
                result.AddError(rowIndex, $"Product with public id '{row.ProductPublicId}' doesn't exist.");
                return;
            }

            var imagePublicId = row.ImagePublicId;
            if (!_imageCache.TryGetValue(imagePublicId, out var image))
            {
                result.AddError(rowIndex, $"Image with public id '{row.ImagePublicId}' doesn't exist.");
                return;
            }

            var productImage = _mapper.Map<ProductImage>(row)
                .SetProduct(product.Id)
                .SetImage(image.Id);
            _productImageRepository.Add(productImage);

            result.SuccessCount++;
        }
    }
}
