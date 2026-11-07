using API.Domain.Common.Repositories;

namespace API.Domain.Features.Catalog.Brands
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Brand?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Brand?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Brand?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default);
        Task<HashSet<string>> GetExistingNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);

        //Task<IEnumerable<Brand>> ToListWithImagesAsync(CancellationToken cancellationToken = default);
    }
}
