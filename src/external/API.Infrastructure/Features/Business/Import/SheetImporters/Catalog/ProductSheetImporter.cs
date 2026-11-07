using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Products;
using API.Domain.Features.Catalog.Brands;
using API.Domain.Features.Catalog.Categories;
using API.Domain.Features.Catalog.Units;
using AutoMapper;
using API.Application.Features.Business;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Catalog
{
    internal class ProductSheetImporter : BaseSheetImporter<ProductRow>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        private readonly Dictionary<Guid, Category> _categoryCache = [];
        private readonly Dictionary<Guid, Brand> _brandCache = [];
        private readonly Dictionary<Guid, Unit> _unitCache = [];
        private readonly HashSet<string> _seenProductNames = new(StringComparer.OrdinalIgnoreCase);

        public ProductSheetImporter(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IBrandRepository brandRepository,
            IUnitRepository unitRepository) : base(unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _unitRepository = unitRepository;
        }

        public override string SheetName => "Products";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<ProductRow> rows,
            CancellationToken cancellationToken)
        {
            _categoryCache.Clear();
            _brandCache.Clear();
            _unitCache.Clear();
            _seenProductNames.Clear();

            var categories = await _categoryRepository.ToListAsync(cancellationToken);
            foreach (var category in categories)
            {
                _categoryCache[category.PublicId] = category;
            }

            var brands = await _brandRepository.ToListAsync(cancellationToken);
            foreach (var brand in brands)
            {
                _brandCache[brand.PublicId] = brand;
            }

            var units = await _unitRepository.ToListAsync(cancellationToken);
            foreach (var unit in units)
            {
                _unitCache[unit.PublicId] = unit;
            }
        }

        protected override async Task ProcessRowAsync(
            ProductRow row,
            int rowIndex,
            ImportResultResponse result,
            CancellationToken cancellationToken)
        {
            var name = row.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                result.AddError(rowIndex, "Name is required and must not be empty.");
                return;
            }

            if (!_seenProductNames.Add(name))
            {
                result.AddError(rowIndex, $"Product with name '{name}' is duplicated in the file.");
                return;
            }

            var categoryPublicId = row.CategoryPublicId;
            if (!_categoryCache.TryGetValue(categoryPublicId, out var category))
            {
                result.AddError(rowIndex, $"Category with public id '{row.CategoryPublicId}' doesn't exist.");
                return;
            }

            Brand? brand = null;
            var brandPublicId = row.BrandPublicId;
            if (brandPublicId != null)
            {
                if (!_brandCache.TryGetValue(brandPublicId.Value, out brand))
                {
                    result.AddError(rowIndex, $"Brand with public id '{row.BrandPublicId}' doesn't exist.");
                    return;
                }
            }

            var unitPublicId = row.UnitPublicId;
            if (!_unitCache.TryGetValue(unitPublicId, out var unit))
            {
                result.AddError(rowIndex, $"Unit with public id '{row.UnitPublicId}' doesn't exist.");
                return;
            }

            row.Name = name;
            row.Description = row.Description?.Trim();

            var product = _mapper.Map<Product>(row);
            product.SetCategory(category.Id);
            product.SetBrand(brand?.Id);
            product.SetUnit(unit.Id);
            _productRepository.Add(product);

            result.SuccessCount++;
        }
    }
}
