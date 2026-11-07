using Microsoft.EntityFrameworkCore;
using API.Infrastructure.Context;
using API.Domain.Features.Catalog.Categories;
using API.Infrastructure.Common.Repositories;

namespace API.Infrastructure.Features.Catalog.Categories
{
    internal class CategoryRepository(AppDbContext context)
        : Repository<Category>(context), ICategoryRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
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

            var existingNames = await _context.Categories
                .Where(x => nameList.Contains(x.Name.ToLower()))
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            return existingNames.ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        public async Task<Category?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Category?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Category?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        //public async Task<IEnumerable<Category>> ToListWithImagesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await _context.Categories
        //        .Include(pc => pc.CategoryImages)
        //            .ThenInclude(pci => pci.Image)
        //        .ToListAsync(cancellationToken);
        //}
    }
}
