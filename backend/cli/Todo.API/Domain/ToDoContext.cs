namespace Todo.API.Domain;

using Microsoft.EntityFrameworkCore;
public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
    }

    public DbSet<ToDo> Todos { get; set; }
}