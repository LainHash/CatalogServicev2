using API.Domain.Features.Storage.ProductImages;
using API.Infrastructure.Common.Repositories;
using API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Features.Storage.ProductImages
{
    internal class ProductImageRepository(AppDbContext context)
        : Repository<ProductImage>(context), IProductImageRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<int> CountByProductIdAsync(long productId, CancellationToken cancellationToken = default)
        {
            return await _context.ProductImages
                .CountAsync(pi => pi.ProductId == productId, cancellationToken);
        }

        public async Task UnsetPrimaryAsync(long productId, CancellationToken cancellationToken = default)
        {
            var primaryImages = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .Include(pi => pi.Image)
                .Where(pi => pi.IsPrimary)
                .Select(pi => pi.Image)
                .ToListAsync(cancellationToken);

            foreach (var image in primaryImages)
            {
                image.ProductImage.RemovePrimary();
            }
        }

        public async Task<Dictionary<Guid, ProductImage>> GetPrimaryImagesByProductPublicIdsAsync(
            IEnumerable<Guid> productPublicIds,
            CancellationToken cancellationToken = default)
        {
            var list = productPublicIds.Distinct().ToList();
            if (list.Count == 0)
            {
                return new Dictionary<Guid, ProductImage>();
            }

            var primaryImages = await _context.ProductImages
                .AsNoTracking()
                .Include(pi => pi.Image)
                .Include(pi => pi.Product)
                .Where(pi => pi.IsPrimary && list.Contains(pi.Product.PublicId))
                .ToListAsync(cancellationToken);

            return primaryImages
                .DistinctBy(pi => pi.Product.PublicId)
                .ToDictionary(pi => pi.Product.PublicId, pi => pi);
        }

        public async Task<Dictionary<long, ProductImage>> GetPrimaryImagesByProductIdsAsync(
            IEnumerable<long> productIds,
            CancellationToken cancellationToken = default)
        {
            var list = productIds.Distinct().ToList();
            if (list.Count == 0)
            {
                return new Dictionary<long, ProductImage>();
            }

            var primaryImages = await _context.ProductImages
                .AsNoTracking()
                .Include(pi => pi.Image)
                .Where(pi => pi.IsPrimary && list.Contains(pi.ProductId))
                .ToListAsync(cancellationToken);

            return primaryImages.ToDictionary(pi => pi.ProductId, pi => pi);
        }
    }
}
