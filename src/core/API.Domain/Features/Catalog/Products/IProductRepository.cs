using API.Domain.Common.Repositories;

namespace API.Domain.Features.Catalog.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
