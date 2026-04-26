using System.Net.Http.Json;
using Microsoft.Extensions.Http;
using ProductWeb.Models;

namespace ProductWeb.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("ProductApi");
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _http.GetFromJsonAsync<List<Product>>("api/products") ?? new List<Product>();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _http.GetFromJsonAsync<Product>($"api/products/{id}");
    }

    public async Task CreateProductAsync(Product product)
    {
        var response = await _http.PostAsJsonAsync("api/products", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var response = await _http.PutAsJsonAsync($"api/products/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/products/{id}");
        response.EnsureSuccessStatusCode();
    }
}
