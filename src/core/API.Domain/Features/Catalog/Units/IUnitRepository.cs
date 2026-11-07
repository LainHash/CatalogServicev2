using API.Domain.Common.Repositories;

namespace API.Domain.Features.Catalog.Units
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<Unit?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<Unit?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Unit?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(Guid id, string name, CancellationToken cancellationToken = default);
        Task<HashSet<string>> GetExistingNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    }
}
