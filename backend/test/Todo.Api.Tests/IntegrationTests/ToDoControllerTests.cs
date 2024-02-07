using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Todo.Cli.Tests.IntegrationTests;

public class ToDoControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    public ToDoControllerTests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }
    
    [Fact]
    public async Task Get_FindManyAsync_NoCompletionStatus_ReturnsAllToDos()
    {
        var response = await _httpClient.GetAsync("todo.findMany");
        var stringResult = await response.Content.ReadAsStringAsync();
        stringResult.Should().NotBeNull();
    }
    
    // [Fact]
    // public async 
}