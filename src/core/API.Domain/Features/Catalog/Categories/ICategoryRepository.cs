using API.Domain.Common.Repositories;

namespace API.Domain.Features.Catalog.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //Task<IEnumerable<Category>> ToListWithImagesAsync(CancellationToken cancellationToken = default);

        Task<Category?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Category?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Category?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default);
        Task<HashSet<string>> GetExistingNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    }
}
