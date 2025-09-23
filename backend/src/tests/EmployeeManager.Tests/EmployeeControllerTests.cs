using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

public class EmployeeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EmployeeControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task LoginEndpoint_ShouldReturnToken()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/login", new {
            Email = "admin@company.com",
            Password = "Teste@123"
        });

        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadAsStringAsync();
        token.Should().NotBeNullOrEmpty();
    }
}
