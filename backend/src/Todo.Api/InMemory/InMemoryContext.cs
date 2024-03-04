using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain.Todo;

namespace Todo.Api.InMemory;

public class InMemoryContext : DbContext
{
    public InMemoryContext(DbContextOptions<InMemoryContext> options)
        : base(options)
    {
    }

    public DbSet<ToDoDocument>? Todos { get; set; }
}