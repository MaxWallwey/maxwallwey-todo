using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain;
using Refit;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(opt =>
    opt.UseInMemoryDatabase("ToDoList"));

//builder.Services.AddRefitClient<IToDoClient>()
//    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:9000"));

builder.Services.AddScoped<IToDoRepository, InMemoryToDoRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Todo.Api
{
    public partial class Program { }
}