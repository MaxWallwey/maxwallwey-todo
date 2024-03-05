using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;

namespace Todo.Api.InMemory;

public class InMemoryRepository : IDocumentRepository
{
    private readonly InMemoryContext _context;

    public InMemoryRepository(InMemoryContext context)
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

    public async Task<List<ToDoDocument>?> FindManyAsync(bool? isComplete)
    {
        if (_context.Todos != null)
        {
            return await _context.Todos.Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync();
        }
        
        return null;
    }

    public async Task<ToDoDocument?> FindOneToDoAsync(string id)
    {
        if (_context.Todos != null)
        {
            var todo = await _context.Todos.FindAsync(ObjectId.Parse(id));
            if (todo != null) return todo;
        }

        return null;
    }
    
    public async Task CompleteToDoAsync(string id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null) todo.Complete();

        await _context.SaveChangesAsync();
    }

    public async Task<string> AddToDoAsync(string name)
    {
        var todo = new ToDoDocument(name);

        _context.Todos!.Add(todo);

        await _context.SaveChangesAsync();

        return todo.Id.ToString();
    }

    public async Task RemoveToDoAsync(string id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null)
        {
            _context.Todos!.Remove(todo);
        }

        await _context.SaveChangesAsync();
    }
}