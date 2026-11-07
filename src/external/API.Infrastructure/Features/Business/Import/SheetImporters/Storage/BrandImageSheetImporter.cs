using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Storage;
using API.Domain.Features.Catalog.Brands;
using API.Domain.Features.Storage.BrandImages;
using API.Domain.Features.Storage.Images;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Storage
{
    internal class BrandImageSheetImporter 
        : BaseSheetImporter<BrandImageRow>
    {
        private readonly IBrandImageRepository _brandImageRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        private readonly Dictionary<Guid, Brand> _brandCache = [];
        private readonly Dictionary<Guid, Image> _imageCache = [];

        public BrandImageSheetImporter(
            IBrandImageRepository brandImageRepository,
            IBrandRepository brandRepository,
            IImageRepository imageRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _brandImageRepository = brandImageRepository;
            _brandRepository = brandRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public override string SheetName => "BrandImages";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<BrandImageRow> rows,
            CancellationToken cancellationToken)
        {
            _brandCache.Clear();
            _imageCache.Clear();

            var brands = await _brandRepository.ToListAsync(cancellationToken);
            foreach (var brand in brands)
            {
                _brandCache[brand.PublicId] = brand;
            }

            var images = await _imageRepository.ToListAsync(cancellationToken);
            foreach (var image in images)
            {
                _imageCache[image.PublicId] = image;
            }
        }

        protected async override Task ProcessRowAsync(
            BrandImageRow row,
            int rowIndex,
            ImportResultResponse result,
            CancellationToken cancellationToken)
        {
            var brandPublicId = row.BrandPublicId;
            if (!_brandCache.TryGetValue(brandPublicId, out var brand))
            {
                result.AddError(rowIndex, $"Brand with public id '{row.BrandPublicId}' doesn't exist.");
                return;
            }

            var imagePublicId = row.ImagePublicId;
            if (!_imageCache.TryGetValue(imagePublicId, out var image))
            {
                result.AddError(rowIndex, $"Image with public id '{row.ImagePublicId}' doesn't exist.");
                return;
            }

            var brandImage = _mapper.Map<BrandImage>(row)
                .SetBrand(brand.Id)
                .SetImage(image.Id);
            _brandImageRepository.Add(brandImage);

            result.SuccessCount++;
        }
    }
}
