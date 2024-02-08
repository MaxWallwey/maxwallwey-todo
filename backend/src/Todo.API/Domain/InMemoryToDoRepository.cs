using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    public Task<List<ToDo>> FindManyAsync(bool? isComplete)
    {
        return _context.Todos.Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync();
    }

    public async Task<ToDo?> FindToDoAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        return todo ?? null;
    }

    public async Task CompleteToDoAsync(Guid id)
    {
        var todo = await FindToDoAsync(id);

        if (todo == null)
        {
            return;
        }
        
        todo.Complete();
        
        await _context.SaveChangesAsync();
    }

    public async Task<Guid> AddToDoAsync(CreateToDo toDo)
    {
        var todo = new ToDo(toDo.Name!);

        _context.Todos.Add(todo);

        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task RemoveToDoAsync(Guid id)
    {
        var todo = await FindToDoAsync(id);

        if (todo == null)
        {
            return;}
        
        _context.Todos.Remove(todo);

        await _context.SaveChangesAsync();
    }
}