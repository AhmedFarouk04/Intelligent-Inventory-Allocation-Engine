using System.Net;
using System.Text.Json;
using FluentAssertions;
using System.Net.Http.Json;

namespace InventoryWarehouseSystem.IntegrationTests.Api;

public class ProductsControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductsControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/v1/products");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.Contains("X-Correlation-ID").Should().BeTrue();
    }

    [Fact]
    public async Task CreateProduct_InvalidPayload_ReturnsBadRequest_WithErrors()
    {
        var response = await _client.PostAsJsonAsync("/api/v1/products", new
        {
            sku = "",
            name = "",
            description = (string?)null,
            categoryId = 0,
            reorderLevel = -1
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Headers.Contains("X-Correlation-ID").Should().BeTrue();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        doc.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        doc.RootElement.GetProperty("errors").GetArrayLength().Should().BeGreaterThan(0);
    }
}
