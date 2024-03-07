using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;
using Bogus.DataSets;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using Moq;
using ToDo.Api.Sdk;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Todo.Api.Tests;

public abstract class ControllerFixture : IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions _jsonSerializerOptions;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    
    public Mock<IMediator> MediatorMock;

    public ControllerFixture()
    {
        var mediatorMock = new Mock<IMediator>();

        _webApplicationFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder
                    .ConfigureAppConfiguration((_, configurationBuilder) =>
                    {
                        var configuration = new Dictionary<string, string>
                        {
                            { "Seq:ServerUrl", "http://localhost:8080/" }
                        };
                        configurationBuilder.AddInMemoryCollection(configuration);
                    })
                    .ConfigureTestServices(services =>
                    {
                        services.AddTransient(_ => mediatorMock.Object);
                        services.AddAuthentication(options =>
                        {
                            options.DefaultChallengeScheme = "FakeBearer";
                            options.DefaultSignInScheme = "FakeBearer";
                            options.DefaultAuthenticateScheme = "FakeBearer";
                        })
                        .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("FakeBearer", _ => { });
                    })
                    .UseEnvironment("Test");
            });

        _httpClient = _webApplicationFactory.CreateClient();

        Faker faker = new Faker();
        Lorem lorem = new Lorem();
        MediatorMock = mediatorMock;

        _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        _jsonSerializerOptions.Converters.Add(new ObjectIdConverter());
    }

    public async Task<TResponse?> DeserializeResponseBodyAsync<TResponse>(HttpResponseMessage httpResponseMessage)
        where TResponse : class
    {
        try
        {
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseBody, _jsonSerializerOptions);
        }
        catch
        {
            return default;
        }
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DisposeCoreAsync();

        _webApplicationFactory.Dispose();
    }

    protected virtual Task DisposeCoreAsync() => Task.CompletedTask;

    public async Task InitializeAsync() => await InitializeCoreAsync();

    protected abstract Task InitializeCoreAsync();

    public async Task<HttpResponseMessage> GetAsync(string requestUri, string? userId)
    {
        if (!string.IsNullOrWhiteSpace(userId))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeBearer", $"FakeToken-{ObjectId.GenerateNewId()}");
            _httpClient.DefaultRequestHeaders.Add("X-UserId", userId);
        }

        return await _httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, string? userId, object? requestBody=null)
    {
        if (!string.IsNullOrWhiteSpace(userId))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FakeBearer", $"FakeToken-{ObjectId.GenerateNewId()}");
            _httpClient.DefaultRequestHeaders.Add("X-UserId", userId);
        }

        return await _httpClient.PostAsync(requestUri, new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") },
        });
    }

    private class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public FakeAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];

            if (string.IsNullOrWhiteSpace(authorization))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = new List<Claim>();
            var userId = Request.Headers["X-UserId"].FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}