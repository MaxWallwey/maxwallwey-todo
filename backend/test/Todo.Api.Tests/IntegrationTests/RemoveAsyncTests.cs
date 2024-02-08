using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Todo.API.Models;

namespace Todo.Cli.Tests.IntegrationTests;

public class RemoveAsyncTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RemoveAsyncTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Delete_RemoveAsync_DeletesTodo_ReturnsNoContent()
    {
        using var scope = new AssertionScope();

        var obj = new { name = "removeMock" };
        
        var client = _factory.CreateClient();
        
        var addedTodo = await client.PostAsJsonAsync("/todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<ResponseData<Guid>>();
        
        var response = await client.DeleteAsync($"/todo.remove?id={content?.Data}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var checkDeletion = await client.GetAsync($"/todo.findOne?id={content?.Data}");
        checkDeletion.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}