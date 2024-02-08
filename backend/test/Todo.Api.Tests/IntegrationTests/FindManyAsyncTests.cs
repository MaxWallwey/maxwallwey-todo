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
    public async Task Get_FindManyAsync_ReturnsOK()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/todo.findMany");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}