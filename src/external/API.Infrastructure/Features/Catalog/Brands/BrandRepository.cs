using API.Domain.Features.Catalog.Brands;
using API.Infrastructure.Common.Repositories;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Features.Catalog.Brands
{
    internal class BrandRepository(AppDbContext context)
        : Repository<Brand>(context), IBrandRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default)
        {
            return await _context.Brands
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

            var existingNames = await _context.Brands
                .Where(x => nameList.Contains(x.Name.ToLower()))
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            return existingNames.ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        public async Task<Brand?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Brand?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Brand?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Brands
                .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        //public async Task<IEnumerable<Brand>> ToListWithImagesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await _context.Brands
        //        .Include(b => b.BrandImages)
        //            .ThenInclude(bi => bi.Image)
        //        .ToListAsync(cancellationToken);
        //}
    }
}
