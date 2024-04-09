/*using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Tests.ContractTests.Todo.AddTodo;

public class When_request_is_valid
{
    public class Fixture : TestServerFixture<
        IDocumentRepository<ToDoDocument>>
    {
        private HttpResponseMessage? Response { get; set; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            var request = new HttpRequestMessage(
                method: HttpMethod.Post,
                requestUri: "/todo.add")
            {
                Content = JsonContent.Create(new
                {
                    Name = "test"
                })
            };
            
            Response = await SendAsync(request);
        }

        [Collection(nameof(TestServerFixtureCollection))]
        public class Then : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Then(Fixture fixture)
            {
                _fixture = fixture;
            }

            [Fact]
            public void Response_status_code_is_ok()
            {
                using var _ = new AssertionScope();
                _fixture.Response.Should().NotBeNull();
                _fixture.Response!.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}*/