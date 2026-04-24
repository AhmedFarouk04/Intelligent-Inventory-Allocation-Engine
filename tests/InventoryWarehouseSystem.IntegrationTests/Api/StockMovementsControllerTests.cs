using System.Net;
using FluentAssertions;

namespace InventoryWarehouseSystem.IntegrationTests.Api;

public class StockMovementsControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StockMovementsControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task History_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/v1/stockmovements/history");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
