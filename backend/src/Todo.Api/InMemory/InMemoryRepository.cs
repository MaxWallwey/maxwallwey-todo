using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Todo.Api.Domain.Models;

namespace Todo.Api.Domain.InMemory;

public class InMemoryRepository<TDocument> : IDocumentRepository
    where TDocument : IDocument
{
    private readonly InMemoryContext _context;

    public InMemoryRepository(InMemoryContext context)
    {
        _context = context;
    }
    
    public async Task<List<ToDoDocument>> FindManyAsync(bool? isComplete)
    {
        return new ResponseData<List<ToDoDocument>>(await _context.Todos!.Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync());

    public Task<bool> AnyAsync(string name)
    {
        if (_context.Todos != null && _context.Todos.Any(x => x.Name == name))
        {
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public async Task<ResponseData<List<ToDo>>?> FindManyAsync(bool? isComplete)
    {
        if (_context.Todos != null)
            return new ResponseData<List<ToDo>>(await _context.Todos
                .Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync());
        return null;
    }

    public async Task<ResponseData<ToDoDocument>?> FindOneToDoAsync(Guid id)
    {
        var todo = await _context.Todos!.FindAsync(id);
        
        if (todo != null) return new ResponseData<ToDoDocument>(todo);
        else
        if (_context.Todos != null)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null) return new ResponseData<ToDo>(todo);
        }

        return null;
    }
    
    public async Task CompleteToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null) todo.Data!.Complete();

        await _context.SaveChangesAsync();
    }

    public async Task<ResponseData<Guid>> AddToDoAsync(string name)
    {
        var checkExisting = await _context.Todos!.FirstOrDefaultAsync(i => i.Name == toDo.Name);
        
        if (checkExisting?.Name != null)
        {
            throw new BadHttpRequestException("Error! ToDo already exists.");
        }
        
        var todo = new ToDoDocument(toDo.Name!);
        var todo = new ToDo(name);

        _context.Todos!.Add(todo);

        await _context.SaveChangesAsync();

        return new ResponseData<Guid>(todo.Id);
    }

    public async Task RemoveToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);
        
        _context.Todos!.Remove(todo?.Data!);

        await _context.SaveChangesAsync();
    }
}