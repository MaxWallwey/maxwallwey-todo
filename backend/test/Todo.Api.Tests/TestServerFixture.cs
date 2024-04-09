using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;
using Todo.Api.Tests.ContractTests;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using Moq;

namespace Todo.Api.Tests;

[CollectionDefinition(nameof(TestServerFixture))]
public class TestServerFixtureCollection
{
}

public class TestServerFixture : IAsyncLifetime
{
    private readonly IMediator _mediator;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TestServerFixture() : this(null) { }

    internal TestServerFixture(
        Action<IServiceCollection>? testServiceCollectionAction = null,
        bool followHttpRedirects = true)
    {
        _mediator = Mock.Of<IMediator>();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder
                    .UseEnvironment("ContractTesting")
                    .ConfigureAppConfiguration((_, configBuilder) =>
                    {
                        foreach (var source in configBuilder.Sources)
                        {
                            if (source is JsonConfigurationSource jsonSource)
                            {
                                jsonSource.ReloadOnChange = false;
                            }
                        }
                        configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                        {
                            { "SkipHostedRetryService", true.ToString() },
                            { "MongoDB:ConnectionString", "mongodb://fake/fake" },
                            { "MyEnergi:UserName", "testUserName" },
                            { "MyEnergi:Password", "testPassword" },
                            { "MyEnergi:ClientId", "testClientId" },
                            { "AzureStorage:ConnectionString", "UseDevelopmentStorage=true" },
                            { "AccountPortal:Origin", "https://fake.com" },
                            { "ProxySettings:PrivateChargeIngestionService:Origin", "https://localhost/fake" },
                            { "Okta:Authority", "https://fleetcor-icd.oktapreview.com/oauth2/aus4o23xs7uvug9QE0x7" },
                            { "Okta:Audience", "Funds test" }
                        });
                    })
                    .ConfigureTestServices(services =>
                    {
                        services.AddTransient(_ => _mediator);
                        services
                            .AddAuthentication(options =>
                            {
                                options.DefaultAuthenticateScheme = "FakeBearer";
                                options.DefaultChallengeScheme = "FakeBearer";
                                options.DefaultSignInScheme = "FakeBearer";
                            })
                            .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("FakeBearer", _ => { });

                        testServiceCollectionAction?.Invoke(services);
                    });
            });

        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = followHttpRedirects
        });

        if (_client == null || _client.BaseAddress == null)
        {
            throw new InvalidOperationException();
        }
    }

    private int IAzureClientFactory(Func<object, object> p) => throw new NotImplementedException();

    public Mock<T> GetMockOf<T>() where T : class
    {
        var mock = _factory.Services.GetService<T>()!;
        return Mock.Get(mock);
    }

    public void MockGetItemAsync<TDocument>(ObjectId documentId, Func<ToDoDocument> document)
        where TDocument : DocumentBase
    {
        GetMockOf<IDocumentRepository<TDocument>>()
            .Setup(x => x.FindOneToDoAsync(documentId))
            .ReturnsAsync(document);
    }

    public void MockGetItemAsync<TDocument>(ToDoDocument document)
        where TDocument : DocumentBase
    {
        MockGetItemAsync<ToDoDocument>(document);
    }
    

    public void SetupRequestHandler<TResponse>(TResponse response)
    {
        Mock.Get(_mediator)
            .Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
    }

    public void SetupRequestHandlerFailure<TResponse>(Exception exception)
    {
        Mock.Get(_mediator)
            .Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);
    }

    public TRequest? GetDeserializedRequest<TRequest>() where TRequest : class
    {
        return Mock.Get(_mediator).Invocations?.ElementAtOrDefault(1)?.Arguments.FirstOrDefault() as TRequest;
    }

    /// <summary>
    /// Use this to test requests to areas of the api which depend on OAuth scopes to authorize requests.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="scopes">Scope claims to be added to the auth</param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        string? scopes,
        string? role = null
    )
    {
        if (!string.IsNullOrWhiteSpace(scopes))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("FakeBearer", $"{scopes}");
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            request.Headers.Add("X-Role", role);
        }

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Use this to test requests to areas of the api which depend on role/permission assignments to authorize requests.
    /// To emulate roles/permissions, use `MockCallerRoleAssignment` first.
    /// </summary>
    /// <param name="request">The request to be tested.</param>
    /// <param name="authenticated">Set to <value>False</value> if the request should not be authenticated. Defaults to <value>True</value>.</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        bool authenticated = true
    )
    {
        if (authenticated)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("FakeBearer");
        }
        return await _client.SendAsync(request);
    }

    public Task DisposeAsync()
    {
        _factory?.Dispose();
        return Task.CompletedTask;
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    private class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorization = Request.Headers[HeaderNames.Authorization]!;
            if (string.IsNullOrWhiteSpace(authorization))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = authorization
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(scope => new Claim(JwtClaimTypes.Scope, scope.Trim()))
                .ToList();

            var role = Request.Headers["X-Role"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}

public class TestServerFixture<T> : TestServerFixture
    where T : class
{
    public TestServerFixture(bool followHttpRedirects = true)
        : base(RegisterMockedServicesActionFactory.Create<T>(), followHttpRedirects)
    {
    }
}

public class TestServerFixture<T1, T2> : TestServerFixture where T1 : class where T2 : class
{
    public TestServerFixture()
        : base(RegisterMockedServicesActionFactory.Create<T1, T2>())
    {
    }
}

public class TestServerFixture<T1, T2, T3> : TestServerFixture where T1 : class where T2 : class where T3 : class
{
    public TestServerFixture()
        : base(RegisterMockedServicesActionFactory.Create<T1, T2, T3>())
    {
    }
}

public class TestServerFixture<T1, T2, T3, T4> : TestServerFixture where T1 : class where T2 : class where T3 : class where T4 : class
{
    public TestServerFixture()
        : base(RegisterMockedServicesActionFactory.Create<T1, T2, T3, T4>())
    {
    }
}

public class TestServerFixture<T1, T2, T3, T4, T5> : TestServerFixture where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
{
    public TestServerFixture()
        : base(RegisterMockedServicesActionFactory.Create<T1, T2, T3, T4, T5>())
    {
    }
}

public class TestServerFixture<T1, T2, T3, T4, T5, T6> : TestServerFixture where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class
{
    public TestServerFixture()
        : base(RegisterMockedServicesActionFactory.Create<T1, T2, T3, T4, T5, T6>())
    {
    }
}
