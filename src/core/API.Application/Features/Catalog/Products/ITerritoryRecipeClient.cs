using API.Domain.Common.Enums;

namespace API.Application.Features.Catalog.Products
{
    public interface ITerritoryRecipeClient
    {
        Task<IReadOnlyDictionary<Guid, StockStatus>> GetRecipeStockStatusAsync(
            Guid branchId,
            IEnumerable<Guid>? productIds = null,
            CancellationToken cancellationToken = default);
    }
}

