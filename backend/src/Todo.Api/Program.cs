using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using Todo.Api;
using Todo.Api.Authentication;
using Todo.Api.Authorization;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Mongo;
using Todo.Api.Domain.Todo;
using Todo.Api.HealthChecks;
using Todo.Api.Validation;
using Todo.Api.ModelBinding;
using Todo.Api.Swashbuckle;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ValidationException>();

builder.Services.AddControllers();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:9000";
        options.Audience = "";

        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
        options.TokenValidationParameters.ValidateAudience = false;
    });  

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IUserProfileAccessor, HttpContextUserProfileAccessor>();

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
        
// Mongo Health Checks
var mongoOptions = builder.Configuration.GetSection(MongoOptions.Key).Get<MongoOptions>();

if (mongoOptions?.ConnectionString != null)
{
    builder.Services.AddHealthChecks()
        .AddCheck<MongoDbHealthCheck>("Mongo");
}

builder.Services.AddHealthChecks();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails),
        StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest));
    
    options.ModelBinderProviders.Insert(0, new ObjectIdModelBinderProvider());
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new ObjectIdConverter());
    });

// Uncomment this for in-memory DB
//builder.Services.AddDbContext<InMemoryContext>(opt =>
//    opt.UseInMemoryDatabase("ToDoList"));
//builder.Services.AddTransient<IDocumentRepository, InMemoryRepository>();

// Uncomment this for Mongo DB
builder.Services.AddMongo();
builder.Services.AddTransient<IDatabaseClient, MongoDatabaseClient>();
builder.Services.AddTransient<IDocumentRepository<ToDoDocument>, MongoDbRepository<ToDoDocument>>();

builder.Host.UseSerilog();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
    c.OperationFilter<ObjectIdOperationFilter>();
    c.SchemaFilter<ObjectIdSchemaFilter>();
    
    c.OperationFilter<OAuth2OperationFilter>();
});

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI");
        c.OAuthClientId("todo-api-swagger");
        c.OAuthClientSecret("secret");
        c.OAuthAppName("Todo API Swagger");
        c.OAuthUsePkce();

        app.UseCors("cors.development");
    });
}

app.UseSerilogRequestLogging(opts
    => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

try
{
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "The application could not be started.");
}
finally
{
    Log.CloseAndFlush();
}

namespace Todo.Api
{
    public class Program { }
}