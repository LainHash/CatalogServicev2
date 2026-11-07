using API.Domain.Common.Repositories;

namespace API.Domain.Features.Catalog.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> ToListByPublicIdsAsync(IEnumerable<Guid> publicIds, CancellationToken cancellationToken = default);

        //Task<Product?> FindProductForOrderAsync(
        //    Guid id,
        //    long branchId,
        //    CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> FindProductsForOrderAsync(
            IEnumerable<Guid> productIds,
            long branchId,
            CancellationToken cancellationToken = default);
    }
}
