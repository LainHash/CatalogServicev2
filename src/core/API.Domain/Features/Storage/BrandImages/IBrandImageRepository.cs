using API.Domain.Common.Repositories;

namespace API.Domain.Features.Storage.BrandImages
{
    public interface IBrandImageRepository : IRepository<BrandImage>
    {
        Task<int> CountByBrandIdAsync(long brandId, CancellationToken cancellationToken = default);

        Task UnsetPrimaryAsync(long brandId, CancellationToken cancellationToken = default);
    }
}
