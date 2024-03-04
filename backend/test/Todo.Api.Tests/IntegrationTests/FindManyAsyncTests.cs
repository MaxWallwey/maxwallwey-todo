using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Todo.Api.Slices.Todo;

namespace Todo.Api.Tests.IntegrationTests;

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
        var obj = new { name = "mock1" };
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/todo.add", obj);
        
        var response = await client.GetAsync("/todo.findMany");

        var content = await response.Content.ReadFromJsonAsync<FindManyToDo.Response>();

        content?.Data.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task FindManyAsync_CompletionStatusTrue_ReturnsOK()
    {
        using var scope = new AssertionScope();
        var obj = new { name = "mock2" };
        var client = _factory.CreateClient();
        var addedTodo = await client.PostAsJsonAsync("/todo.add", obj);
        var id = await addedTodo.Content.ReadFromJsonAsync<AddToDo.Response>();

        var idObj = new CompleteToDo.CompleteToDoRequest(id!.Data);
        
        var request = new HttpRequestMessage {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/todo.complete", UriKind.Relative),
            Content = new StringContent(JsonConvert.SerializeObject(idObj), Encoding.UTF8, "application/json")
        };
        var responseComplete = await client.SendAsync(request);
        
        var response = await client.GetAsync("/todo.findMany?isComplete=true");

        var content = await response.Content.ReadFromJsonAsync<FindManyToDo.Response>();

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

        var content = await response.Content.ReadFromJsonAsync<FindManyToDo.Response>();

        content?.Data.Should().NotBeNullOrEmpty();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}