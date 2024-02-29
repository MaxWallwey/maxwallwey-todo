using Microsoft.EntityFrameworkCore;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;
using Todo.Api.Models;

namespace Todo.Api.Domain.InMemory;

public class InMemoryRepository<TDocument> : IDocumentRepository
    where TDocument : IDocument
{
    private readonly InMemoryContext _context;

    public InMemoryRepository(InMemoryContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseData<List<ToDoDocument>>> FindManyAsync(bool? isComplete)
    {
        return new ResponseData<List<ToDoDocument>>(await _context.Todos!.Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync());
    }

    public async Task<ResponseData<ToDoDocument>?> FindOneToDoAsync(Guid id)
    {
        var todo = await _context.Todos!.FindAsync(id);
        
        if (todo != null) return new ResponseData<ToDoDocument>(todo);
        else
        {
            throw new BadHttpRequestException("Error! ToDo was not found.");
        }
    }

    public async Task CompleteToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);

        if (todo != null) todo.Data!.Complete();
        else
        {
            throw new BadHttpRequestException("Error! ToDo was not found.");
        }

        await _context.SaveChangesAsync();
    }

    public async Task<ResponseData<Guid>> AddToDoAsync(CreateToDo toDo)
    {
        var checkExisting = await _context.Todos!.FirstOrDefaultAsync(i => i.Name == toDo.Name);
        
        if (checkExisting?.Name != null)
        {
            throw new BadHttpRequestException("Error! ToDo already exists.");
        }
        
        var todo = new ToDoDocument(toDo.Name!);

        _context.Todos!.Add(todo);

        await _context.SaveChangesAsync();

        return new ResponseData<Guid>(todo.Id);
    }

    public async Task RemoveToDoAsync(Guid id)
    {
        var todo = await FindOneToDoAsync(id);
        
        if (todo != null) _context.Todos!.Remove(todo.Data!);
        else
        {
            throw new BadHttpRequestException("Error! ToDo was not found.");
        }

        await _context.SaveChangesAsync();
    }
}