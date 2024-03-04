using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Todo.Api.Domain;
using Todo.Api.Slices.Todo;

namespace Todo.Api.Tests.IntegrationTests;

public class CompleteAsyncTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CompleteAsyncTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CompleteAsync_ValidTask_ReturnsNoContent()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "completeMock" };
        var client = _factory.CreateClient();
        var addedTodo = await client.PostAsJsonAsync("file:///todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<AddToDo.Response>();

        var idObj = new CompleteToDo.CompleteToDoRequest(content!.Data);

        var request = new HttpRequestMessage {
            Method = HttpMethod.Post,
            RequestUri = new Uri("file:///todo.complete"),
            Content = new StringContent(JsonConvert.SerializeObject(idObj), Encoding.UTF8, "application/json")
        };
        var responseComplete = await client.SendAsync(request);

        responseComplete.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task CompleteAsync_InvalidTask_ReturnsBadRequest()
    {
        using var scope = new AssertionScope();
        
        var client = _factory.CreateClient();
        
        var idObj = new CompleteToDo.CompleteToDoRequest(Guid.Empty);

        var request = new HttpRequestMessage {
            Method = HttpMethod.Post,
            RequestUri = new Uri("file:///todo.complete"),
            Content = new StringContent(JsonConvert.SerializeObject(idObj), Encoding.UTF8, "application/json")
        };
        var responseComplete = await client.SendAsync(request);

        responseComplete.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}