using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Todo.API.Models;

namespace Todo.Cli.Tests.IntegrationTests;

public class AddAsyncTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AddAsyncTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Post_AddAsync_AddsNewTodo_ReturnsOK()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "addMock" };
        
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/todo.add", obj);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<ResponseData<Guid>>();

        content?.Data.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Post_AddAsync_ValidationErrorsReturnBadRequest()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "" };
        
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/todo.add", obj);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}