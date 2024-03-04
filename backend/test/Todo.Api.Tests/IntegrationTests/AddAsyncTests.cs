using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Todo.Api.Slices.Todo;

namespace Todo.Api.Tests.IntegrationTests;

public class AddAsyncTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AddAsyncTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task AddAsync_ValidTask_ReturnsOK()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "addMock1" };
        
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/todo.add", obj);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<AddToDo.Response>();

        content?.Data.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task AddAsync_MissingName_ReturnBadRequest()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "" };
        
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/todo.add", obj);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}