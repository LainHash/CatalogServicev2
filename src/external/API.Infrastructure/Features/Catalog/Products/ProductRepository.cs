using Microsoft.EntityFrameworkCore;
using API.Infrastructure.Context;
using API.Domain.Features.Catalog.Products;
using API.Infrastructure.Common.Repositories;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class ProductRepository(AppDbContext context)
        : Repository<Product>(context), IProductRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Product?> FindByIdAsync(
            Guid id, 
            CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }
    }
}
