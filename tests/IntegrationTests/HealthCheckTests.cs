using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public HealthCheckTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateDefaultClient();
    }

    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/healthcheck");

        response.EnsureSuccessStatusCode();
    }
}