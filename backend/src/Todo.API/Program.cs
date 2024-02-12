using Microsoft.EntityFrameworkCore;
using Todo.API.Domain;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(opt =>
    opt.UseInMemoryDatabase("ToDoList"));

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

public partial class Program { }