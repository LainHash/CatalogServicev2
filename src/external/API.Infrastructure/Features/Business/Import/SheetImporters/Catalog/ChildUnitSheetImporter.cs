using API.Application.Features.Business;
using API.Contract.DTOs.Business.Import;
using API.Contract.ImportDataRows.Catalog;
using API.Domain.Features.Catalog.Units;
using AutoMapper;

namespace API.Infrastructure.Features.Business.Import.SheetImporters.Catalog
{
    internal class ChildUnitSheetImporter
        : BaseSheetImporter<ChildUnitRow>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<Guid, Unit> _unitCache = [];
        private HashSet<string> _existingNames = new(StringComparer.OrdinalIgnoreCase);

        public ChildUnitSheetImporter(
            IUnitRepository unitRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public override string SheetName => "ChildUnits";

        protected override async Task BeforeProcessRowsAsync(
            IReadOnlyList<ChildUnitRow> rows,
            CancellationToken cancellationToken)
        {
            _unitCache.Clear();

            var units = await _unitRepository.ToListAsync(cancellationToken);
            foreach (var unit in units)
            {
                _unitCache[unit.PublicId] = unit;
            }

            var namesToCheck = rows
                .Select(r => r.Name?.Trim())
                .OfType<string>()
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            _existingNames = await _unitRepository
                .GetExistingNamesAsync(namesToCheck, cancellationToken);
        }

        protected override Task ProcessRowAsync(
            ChildUnitRow row,
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

            if (!_unitCache.TryGetValue(row.ParentPublicId, out var parentUnit))
            {
                result.AddError(rowIndex, $"Parent unit with public id '{row.ParentPublicId}' doesn't exist.");
                return Task.CompletedTask;
            }

            _existingNames.Add(name);

            row.Name = name;
            row.Symbol = row.Symbol?.Trim()!;

            var unit = _mapper.Map<Unit>(row);
            unit.SetParent(parentUnit.Id);
            _unitRepository.Add(unit);

            result.SuccessCount++;
            return Task.CompletedTask;
        }
    }
}

