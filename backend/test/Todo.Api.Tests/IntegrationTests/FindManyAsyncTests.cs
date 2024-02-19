using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Todo.API.Models;

namespace Todo.Cli.Tests.IntegrationTests;

public class FindManyAsyncTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public FindManyAsyncTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    // FindMany Endpoint
    [Fact]
    public async Task FindManyAsync_NoCompletionStatus_ReturnsOK()
    {
        using var scope = new AssertionScope();
        var obj = new { name = "mock" };
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/todo.add", obj);
        
        var response = await client.GetAsync("/todo.findMany");

        var content = await response.Content.ReadFromJsonAsync<ResponseData<List<ToDo.API.SDK.ToDo>>>();

        content?.Data.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task FindManyAsync_CompletionStatusTrue_ReturnsOK()
    {
        using var scope = new AssertionScope();
        var obj = new { name = "mock" };
        var client = _factory.CreateClient();
        var addedTodo = await client.PostAsJsonAsync("/todo.add", obj);
        var id = await addedTodo.Content.ReadFromJsonAsync<ResponseData<Guid>>();
        
        await client.PostAsync($"/todo.complete?id={id?.Data}", null);
        
        var response = await client.GetAsync("/todo.findMany?isComplete=true");

        var content = await response.Content.ReadFromJsonAsync<ResponseData<List<ToDo.API.SDK.ToDo>>>();

        content?.Data.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task FindManyAsync_CompletionStatusFalse_ReturnsOK()
    {
        using var scope = new AssertionScope();
        var obj = new { name = "mock" };
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/todo.add", obj);
        
        var response = await client.GetAsync("/todo.findMany?isComplete=False");

        var content = await response.Content.ReadFromJsonAsync<ResponseData<List<ToDo.API.SDK.ToDo>>>();

        content?.Data.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}