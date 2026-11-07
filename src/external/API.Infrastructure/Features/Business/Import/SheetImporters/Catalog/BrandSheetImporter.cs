using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Brands;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Catalog
{
    internal class BrandSheetImporter : BaseSheetImporter<BrandRow>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private HashSet<string> _existingNames = new(StringComparer.OrdinalIgnoreCase);

        public BrandSheetImporter(
            IBrandRepository brandRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public override string SheetName => "Brands";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<BrandRow> rows,
            CancellationToken cancellationToken)
        {
            var namesToCheck = rows
                .Select(r => r.Name?.Trim())
                .OfType<string>()
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            _existingNames = await _brandRepository
                .GetExistingNamesAsync(namesToCheck, cancellationToken);
        }

        protected override Task ProcessRowAsync(
            BrandRow row,
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
                result.AddError(rowIndex, $"Brand with name '{name}' already exists.");
                return Task.CompletedTask;
            }

            _existingNames.Add(name);

            row.Name = name;
            row.Description = row.Description?.Trim();

            var brand = _mapper.Map<Brand>(row);
            _brandRepository.Add(brand);

            result.SuccessCount++;
            return Task.CompletedTask;
        }
    }
}
