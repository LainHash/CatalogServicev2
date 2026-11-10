using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Application.Features.Catalog.Products;
using API.Domain.Common.Enums;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class TerritoryRecipeClient(HttpClient httpClient) : ITerritoryRecipeClient
    {
        private readonly HttpClient _httpClient = httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public async Task<IReadOnlyDictionary<Guid, StockStatus>> GetRecipeStockStatusAsync(
            Guid branchId,
            IEnumerable<Guid>? productIds = null,
            CancellationToken cancellationToken = default)
        {
            var queryParams = new List<string>
            {
                $"branchId={branchId}"
            };

            if (productIds is not null)
            {
                foreach (var productId in productIds.Where(id => id != Guid.Empty).Distinct())
                {
                    queryParams.Add($"productIds={productId}");
                }
            }

            var url = $"/api/territory/recipes/stock-status?{string.Join("&", queryParams)}";

            using var response = await _httpClient.GetAsync(url, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new Dictionary<Guid, StockStatus>();
            }

            var wrapper = await response.Content.ReadFromJsonAsync<TerritoryApiResult<List<ProductRecipeStockStatusDto>>>(
                JsonOptions,
                cancellationToken);

            if (wrapper?.Data is null)
            {
                return new Dictionary<Guid, StockStatus>();
            }

            return wrapper.Data
                .GroupBy(x => x.ProductPublicId)
                .ToDictionary(g => g.Key, g => g.First().StockStatus);
        }

        private sealed class TerritoryApiResult<T>
        {
            public bool IsSucceed { get; set; }
            public string? Message { get; set; }
            public int StatusCode { get; set; }
            public T? Data { get; set; }
        }

        private sealed class ProductRecipeStockStatusDto
        {
            public Guid ProductPublicId { get; set; }
            public StockStatus StockStatus { get; set; }
        }
    }
}

