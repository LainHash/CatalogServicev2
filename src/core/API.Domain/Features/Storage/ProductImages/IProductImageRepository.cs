using API.Domain.Common.Repositories;

namespace API.Domain.Features.Storage.ProductImages
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<int> CountByProductIdAsync(long productId, CancellationToken cancellationToken = default);

        Task UnsetPrimaryAsync(long productId, CancellationToken cancellationToken = default);

        Task<Dictionary<Guid, ProductImage>> GetPrimaryImagesByProductPublicIdsAsync(
            IEnumerable<Guid> productPublicIds,
            CancellationToken cancellationToken = default);

        Task<Dictionary<long, ProductImage>> GetPrimaryImagesByProductIdsAsync(
            IEnumerable<long> productIds,
            CancellationToken cancellationToken = default);
    }
}
