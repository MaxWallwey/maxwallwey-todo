using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Domain;

public class InMemoryToDoRepository : IToDoRepository
{
    private readonly ToDoContext _context;

    public InMemoryToDoRepository(ToDoContext context)
    {
        _context = context;
    }
    
    public async Task<List<ToDo>> FindMany(bool? isComplete)
    {
        return (await _context.Todos.Where(i => isComplete == null || i!.IsComplete == isComplete).ToListAsync())!;
    }

    public async Task<ToDo?> FindToDo(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public async Task CompleteToDo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        todo!.Complete();

        await _context.SaveChangesAsync();
    }

    public async Task<Guid> AddToDo(CreateToDo toDo)
    {
        var todo = new ToDo(toDo.Name!);

        _context.Todos.Add(todo);

        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task RemoveToDo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();
    }
}