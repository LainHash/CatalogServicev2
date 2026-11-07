using API.Domain.Features.Storage.BrandImages;
using API.Infrastructure.Common.Repositories;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Features.Storage.BrandImages
{
    internal class BrandImageRepository(AppDbContext context)
        : Repository<BrandImage>(context), IBrandImageRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<int> CountByBrandIdAsync(long brandId, CancellationToken cancellationToken = default)
        {
            return await _context.BrandImages
                .CountAsync(bi => bi.BrandId == brandId, cancellationToken);
        }

        public async Task UnsetPrimaryAsync(long brandId, CancellationToken cancellationToken = default)
        {
            var primaryImages = await _context.BrandImages
                .Where(bi => bi.BrandId == brandId && bi.IsPrimary)
                .ToListAsync(cancellationToken);

            foreach (var brandImage in primaryImages)
            {
                brandImage.RemovePrimary();
            }
        }
    }
}
