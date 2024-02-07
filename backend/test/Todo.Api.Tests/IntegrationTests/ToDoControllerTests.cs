using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Todo.API.Models;

namespace Todo.Cli.Tests.IntegrationTests;

public class ToDoControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    public ToDoControllerTests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }
    
    // Get Endpoint
    [Fact]
    public async Task Get_FindManyAsync_ReturnsOK()
    {
        var response = await _httpClient.GetAsync("/todo.findMany");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<ResponseData<List<ToDo>>>();
        content?.Data.Should().BeNullOrEmpty();
    }
    
    // Add Endpoint
    [Fact]
    public async Task Post_AddAsync_AddsNewTodo_ReturnsOK()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "mock" };
        
        var response = await _httpClient.PostAsJsonAsync("/todo.add", obj);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<ResponseData<Guid>>();

        content?.Data.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Post_AddAsync_ValidationErrorsReturnBadRequest()
    {
        using var scope = new AssertionScope();
        
        var obj = new { name = "" };
        
        var response = await _httpClient.PostAsJsonAsync("/todo.add", obj);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    // Complete Endpoint
    [Fact]
    public async Task Post_CompleteAsync_SetsCompletionStatusToTrue_ReturnsNoContent()
    {
        using var scope = new AssertionScope();

        var obj = new { name = "mock" };
        var addedTodo = await _httpClient.PostAsJsonAsync("/todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<ResponseData<Guid>>();
        
        var response = await _httpClient.PostAsync($"/todo.complete?id={content?.Data}", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var checkCompletion = await _httpClient.GetAsync("/todo.findMany");
        var content1 = await checkCompletion.Content.ReadFromJsonAsync<ResponseData<List<ToDo>>>();
        content1?.Data.FirstOrDefault()?.IsComplete.Should().Be(true);
    }
    
    [Fact]
    public async Task Post_CompleteAsync_GuidNotFound_ReturnsBadRequest()
    {
        using var scope = new AssertionScope();
        
        var response = await _httpClient.PostAsync($"/todo.complete?id=", null);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Delete_RemoveAsync_DeletesTodo_ReturnsNoContent()
    {
        using var scope = new AssertionScope();

        var obj = new { name = "mock" };
        var addedTodo = await _httpClient.PostAsJsonAsync("/todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<ResponseData<Guid>>();
        
        var response = await _httpClient.DeleteAsync($"/todo.remove?id={content?.Data}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var checkDeletion = await _httpClient.GetAsync("/todo.findMany");
        var content1 = await checkDeletion.Content.ReadFromJsonAsync<ResponseData<List<ToDo>>>();
        content1?.Data.Should().BeNullOrEmpty();
    }
}