using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Categories;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Catalog
{
    public class ChildCategorySheetImporter
        : BaseSheetImporter<ChildCategoryRow>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<Guid, Category> _categoryCache = [];
        private HashSet<string> _existingNames = new(StringComparer.OrdinalIgnoreCase);

        public ChildCategorySheetImporter(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public override string SheetName => "ChildCategories";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<ChildCategoryRow> rows,
            CancellationToken cancellationToken)
        {
            _categoryCache.Clear();

            var categories = await _categoryRepository.ToListAsync(cancellationToken);
            foreach (var category in categories)
            {
                _categoryCache[category.PublicId] = category;
            }

            var namesToCheck = rows
                .Select(r => r.Name?.Trim())
                .OfType<string>()
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            _existingNames = await _categoryRepository
                .GetExistingNamesAsync(namesToCheck, cancellationToken);
        }

        protected override Task ProcessRowAsync(
            ChildCategoryRow row,
            int rowIndex,
            ImportResultResponse result,
            CancellationToken cancellationToken)
        {
            var name = row.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                result.AddError(rowIndex, "Name is required and must not be empty.");
                return Task.CompletedTask;
            }

            if (_existingNames.Contains(name))
            {
                result.AddError(rowIndex, $"Category with name '{name}' already exists.");
                return Task.CompletedTask;
            }

            if (!_categoryCache.TryGetValue(row.ParentPublicId, out var parentCategory))
            {
                result.AddError(rowIndex, $"Parent category with public id '{row.ParentPublicId}' doesn't exist.");
                return Task.CompletedTask;
            }

            _existingNames.Add(name);

            row.Name = name;
            row.Description = row.Description?.Trim();

            var category = _mapper.Map<Category>(row);
            category.SetParent(parentCategory.Id);
            _categoryRepository.Add(category);

            result.SuccessCount++;
            return Task.CompletedTask;
        }
    }
}

