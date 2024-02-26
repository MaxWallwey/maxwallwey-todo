using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Todo.Api.Domain;
using Todo.Api.Models;

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
        var addedTodo = await client.PostAsJsonAsync("/todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<ResponseData<Guid>>();

        var response = await client.PostAsync($"/todo.complete?id={content?.Data}", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var checkCompletion = await client.GetAsync($"/todo.findOne?id={content?.Data}");
        var content1 = await checkCompletion.Content.ReadFromJsonAsync<ResponseData<ToDo>>();
        content1?.Data.IsComplete.Should().Be(true);
    }

    [Fact]
    public async Task CompleteAsync_InvalidTask_ReturnsBadRequest()
    {
        using var scope = new AssertionScope();
        
        var client = _factory.CreateClient();
        
        var response = await client.PostAsync($"/todo.complete?id=", null);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}