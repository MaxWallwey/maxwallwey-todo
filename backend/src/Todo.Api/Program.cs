using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Todo.API;
using Todo.Api.Domain.InMemory;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;
using Todo.Api.Validation;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatR.Extensions.FluentValidation.AspNetCore.ValidationBehavior<,>));

builder.Services.AddControllers();

builder.Services.AddDbContext<InMemoryContext>(opt =>
    opt.UseInMemoryDatabase("ToDoList"));

builder.Services.AddTransient<IDocumentRepository, InMemoryRepository<ToDoDocument>>();

builder.Host.UseSerilog();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseSerilogRequestLogging(opts
    => opts.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application starting.");
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