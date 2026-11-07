using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Units;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Catalog
{
    internal class UnitSheetImporter 
        : BaseSheetImporter<UnitRow>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private HashSet<string> _existingNames = new(StringComparer.OrdinalIgnoreCase);

        public UnitSheetImporter(
            IUnitRepository unitRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public override string SheetName => "Units";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<UnitRow> rows,
            CancellationToken cancellationToken)
        {
            var namesToCheck = rows
                .Select(r => r.Name?.Trim())
                .OfType<string>()
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            _existingNames = await _unitRepository
                .GetExistingNamesAsync(namesToCheck, cancellationToken);
        }

        protected override Task ProcessRowAsync(
            UnitRow row,
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
                result.AddError(rowIndex, $"Unit with name '{name}' already exists.");
                return Task.CompletedTask;
            }

            _existingNames.Add(name);

            row.Name = name;

            var unit = _mapper.Map<Unit>(row);
            _unitRepository.Add(unit);

            result.SuccessCount++;
            return Task.CompletedTask;
        }
    }
}
