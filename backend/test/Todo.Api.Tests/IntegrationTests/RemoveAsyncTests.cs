using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
using Newtonsoft.Json;
using Todo.Api.Slices.Todo;

namespace Todo.Api.Tests.IntegrationTests;

public class RemoveAsyncTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public RemoveAsyncTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task RemoveAsync_ValidTask_ReturnsNoContent()
    {
        using var scope = new AssertionScope();

        var obj = new AddToDo.AddToDoRequest("mock1");
        
        var client = _factory.CreateClient();
        
        var addedTodo = await client.PostAsJsonAsync("/todo.add", obj);
        var content = await addedTodo.Content.ReadFromJsonAsync<AddToDo.Response>();

        var objId = new RemoveToDo.RemoveToDoRequest(content!.Data);
        
        var request = new HttpRequestMessage {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("/todo.remove", UriKind.Relative),
            Content = new StringContent(JsonConvert.SerializeObject(objId), Encoding.UTF8, "application/json")
        };
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var checkDeletion = await client.GetAsync($"/todo.findOne?id={content}");
        checkDeletion.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task RemoveAsync_InvalidTask_ReturnsBadRequest()
    {
        using var scope = new AssertionScope();
        
        var client = _factory.CreateClient();
        
        var objId = new RemoveToDo.RemoveToDoRequest(ObjectId.Empty);
   
        var request = new HttpRequestMessage {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("/todo.remove", UriKind.Relative),
            Content = new StringContent(JsonConvert.SerializeObject(objId), Encoding.UTF8, "application/json")
        };
        var response = await client.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}