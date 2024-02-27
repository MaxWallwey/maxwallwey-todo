using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Models;

namespace Todo.Api.Domain;

public class InMemoryToDoRepository : IToDoRepository
{
    private readonly ToDoContext _context;

    public InMemoryToDoRepository(ToDoContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseData<List<ToDo>>> FindManyAsync(bool? isComplete)
    {
        return new ResponseData<List<ToDo>>(await _context.Todos.Where(i => isComplete == null || i.IsComplete == isComplete).ToListAsync());
    }

    public async Task<ResponseData<ToDo?>> FindToDoAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        return new ResponseData<ToDo?>(todo);
    }

    public async Task CompleteToDoAsync(Guid id)
    {
        var todo = await FindToDoAsync(id);

        if (todo == null)
        {
            return;
        }
        
        todo.Data.Complete();
        
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseData<Guid>> AddToDoAsync(CreateToDo toDo)
    {
        var checkExisting = await _context.Todos.FirstOrDefaultAsync(i => i.Name == toDo.Name);
        
        if (checkExisting?.Name != null)
        {
            throw new Exception("Error! ToDo already exists!");
        }
        
        var todo = new ToDo(toDo.Name!);

        _context.Todos.Add(todo);

        await _context.SaveChangesAsync();

        return new ResponseData<Guid>(todo.Id);
    }

    public async Task RemoveToDoAsync(Guid id)
    {
        var todo = await FindToDoAsync(id);

        if (todo == null)
        {
            throw new Exception("Error! ToDo was not found.");
        }
        
        _context.Todos.Remove(todo.Data);

        await _context.SaveChangesAsync();
    }
}