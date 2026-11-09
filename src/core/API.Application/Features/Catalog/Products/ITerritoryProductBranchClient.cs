using API.Contract.DTOs.Catalog.Products;

namespace API.Application.Features.Catalog.Products
{
    public interface ITerritoryProductBranchClient
    {
        Task<IReadOnlyDictionary<Guid, ProductBranchDto>> GetByBranchAsync(
            Guid branchId,
            IEnumerable<Guid>? productIds = null,
            CancellationToken cancellationToken = default);
    }
}

