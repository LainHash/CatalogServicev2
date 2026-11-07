using API.Domain.Features.Catalog.Units;
using API.Infrastructure.Common.Repositories;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Features.Catalog.Units
{
    internal class UnitRepository(AppDbContext context)
        : Repository<Unit>(context), IUnitRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default)
        {
            return await _context.Units
                .Where(x => x.PublicId != id)
                .AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<HashSet<string>> GetExistingNamesAsync(
            IEnumerable<string> names,
            CancellationToken cancellationToken = default)
        {
            var nameList = names
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.ToLower())
                .Distinct()
                .ToList();

            if (nameList.Count == 0)
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }

            var existingNames = await _context.Units
                .Where(x => nameList.Contains(x.Name.ToLower()))
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            return existingNames.ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        public async Task<Unit?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Unit?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Unit?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Units
                .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}
