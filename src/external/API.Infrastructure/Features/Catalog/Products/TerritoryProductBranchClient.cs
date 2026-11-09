using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Application.Features.Catalog.Products;
using API.Contract.DTOs.Catalog.Products;

namespace API.Infrastructure.Features.Catalog.Products
{
    internal class TerritoryProductBranchClient(HttpClient httpClient) : ITerritoryProductBranchClient
    {
        private readonly HttpClient _httpClient = httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public async Task<IReadOnlyDictionary<Guid, ProductBranchDto>> GetByBranchAsync(
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

            var url = $"/api/territory/product-branches?{string.Join("&", queryParams)}";

            using var response = await _httpClient.GetAsync(url, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new Dictionary<Guid, ProductBranchDto>();
            }

            var wrapper = await response.Content.ReadFromJsonAsync<TerritoryApiResult<List<ProductBranchDto>>>(
                JsonOptions,
                cancellationToken);

            if (wrapper?.Data is null)
            {
                return new Dictionary<Guid, ProductBranchDto>();
            }

            return wrapper.Data
                .GroupBy(x => x.ProductPublicId)
                .ToDictionary(g => g.Key, g => g.First());
        }

        private sealed class TerritoryApiResult<T>
        {
            public bool IsSucceed { get; set; }
            public string? Message { get; set; }
            public int StatusCode { get; set; }
            public T? Data { get; set; }
        }
    }
}

