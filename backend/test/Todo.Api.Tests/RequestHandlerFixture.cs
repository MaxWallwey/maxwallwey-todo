using System.Linq.Expressions;
using Bogus;
using Bogus.DataSets;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using Moq;
using Todo.Api.Authentication;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Mongo;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Tests;

public abstract class RequestHandlerFixture : IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly Mock<IUserProfileAccessor> _userProfileAccessorMock;

    protected Faker Faker { get; }
    protected Lorem Lorem { get; }

    public RequestHandlerFixture()
    {
        _userProfileAccessorMock = new Mock<IUserProfileAccessor>();
        _webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder
                    .ConfigureAppConfiguration((_, configurationBuilder) =>
                    {
                        var configuration = new Dictionary<string, string>
                        {
                            { "Mongo:UseMongo", "true" },
                            { "Mongo:ConnectionString", $"mongodb://localhost:27017/todo-test" },
                            { "Seq:ServerUrl", "http://localhost:5341/" }
                        };
                        configurationBuilder.AddInMemoryCollection(configuration);
                    })
                    .ConfigureTestServices(services =>
                    {
                        services.AddTransient(_ => _userProfileAccessorMock.Object);
                    })
                    .UseEnvironment("Test");
            });

        Faker = new Faker();
        Lorem = new Lorem();
    }

    protected virtual void ConfigureTestServices(IServiceCollection services)
    {
    }

    public async Task CreateOneAsync<TDocument>(ToDoDocument document)
        where TDocument : IDocument
    {
        using var serviceScopeFactory = _webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var repository = serviceScopeFactory.ServiceProvider.GetRequiredService<IDocumentRepository<TDocument>>();

        await repository.AddToDoAsync(document);
    }
    async Task IAsyncLifetime.DisposeAsync()
    {
        await DropDatabaseAsync();

        await DisposeCoreAsync();

        _webApplicationFactory.Dispose();
    }

    protected virtual Task DisposeCoreAsync() => Task.CompletedTask;

    protected async Task DropDatabaseAsync()
    {
        using var serviceScopeFactory = _webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var databaseClient = serviceScopeFactory.ServiceProvider.GetRequiredService<IDatabaseClient>();

        await databaseClient.DropDatabaseAsync(CancellationToken.None);
    }

    public async Task<ToDoDocument?> FindOneAsync<TDocument>(ObjectId id)
        where TDocument : IDocument
    {
        using var serviceScopeFactory = _webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var repository = serviceScopeFactory.ServiceProvider.GetRequiredService<IDocumentRepository<TDocument>>();

        return await repository.FindOneToDoAsync(id);
    }

    public async Task<List<ToDoDocument>?> FindManyAsync<TDocument>(bool filter)
        where TDocument : IDocument
    {
        using var serviceScopeFactory = _webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        var repository = serviceScopeFactory.ServiceProvider.GetRequiredService<IDocumentRepository<TDocument>>();

        return await repository.FindManyAsync(filter);
    }

    public async Task InitializeAsync() => await InitializeCoreAsync();

    protected abstract Task InitializeCoreAsync();

    protected async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, string? userId)
        where TResponse : notnull
    {
        using var serviceScopeFactory = _webApplicationFactory.Services.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        _userProfileAccessorMock.SetupGet(userProfileAccessor => userProfileAccessor.Subject)
            .Returns(userId);

        var mediator = serviceScopeFactory.ServiceProvider.GetRequiredService<IMediator>();

        return await mediator.Send(request);
    }
}