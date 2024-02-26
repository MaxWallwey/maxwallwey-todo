using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Todo.API;
using Todo.API.Domain;


var builder = WebApplication.CreateBuilder(args);
using Todo.Api.Domain;
using Refit;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(opt =>
    opt.UseInMemoryDatabase("ToDoList"));

builder.Services.AddScoped<IToDoRepository, InMemoryToDoRepository>();

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

//app.UseSerilogRequestLogging();

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
    public partial class Program { }
}