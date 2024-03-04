using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Domain;

public class InMemoryToDoRepository : IToDoRepository
{
    private readonly ToDoContext _context;

    public InMemoryToDoRepository(ToDoContext context)
    {
        _context = context;
    }

    public Task<bool> AnyAsync(string name)
    {
        if (_context.Todos != null && _context.Todos.Any(x => x.Name == name))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public async Task<List<ToDo>?> FindManyAsync(bool? isComplete)
    {
        if (_context.Todos != null)
        {
            return await _context.Todos
                .Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync();
        }
        
        return null;
    }

    public async Task<ToDo?> FindOneToDoAsync(Guid id)
    {
        if (_context.Todos != null)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null) return todo;
        }

        return null;
    }
    
    public async Task CompleteToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null) todo.Complete();

        await _context.SaveChangesAsync();
    }

    public async Task<Guid> AddToDoAsync(string name)
    {
        var todo = new ToDo(name);

        _context.Todos!.Add(todo);

        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task RemoveToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null)
        {
            _context.Todos!.Remove(todo);
        }

        await _context.SaveChangesAsync();
    }
}