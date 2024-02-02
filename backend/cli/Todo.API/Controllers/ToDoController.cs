using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly ToDoContext _context;

    public ToDoController(ToDoContext context)
    {
        _context = context;
    }

    // GET: api/ToDo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> GetTodos()
    {
        if (_context.Todos == null) return NotFound();
        return await _context.Todos.ToListAsync();
    }

    // GET: api/ToDo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDo>> GetToDo(Guid id)
    {
        if (_context.Todos == null) return NotFound();
        var toDo = await _context.Todos.FindAsync(id);

        if (toDo == null) return NotFound();

        return toDo;
    }

    //Complete todo
    [HttpPost("complete {id}")]
    public async Task<IActionResult> PutToDo(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        todo?.Complete();

        return NoContent();
    }

    //Add new todo
    [HttpPost ("add{toDo}")]
    public async Task<ActionResult<Guid>> PostToDo(string toDo, bool isComplete = false)
    {
        if (_context.Todos == null) return Problem("Entity set 'ToDoContext.Todos'  is null.");
        var newToDo = new ToDo(toDo, isComplete);
        _context.Todos.Add(newToDo);
        await _context.SaveChangesAsync();

        return newToDo.Id;
    }

    //Remove todo
    [HttpDelete(" remove{id}")]
    public async Task<IActionResult> DeleteToDo(Guid id)
    {
        if (_context.Todos == null) return NotFound();
        var toDo = await _context.Todos.FindAsync(id);
        if (toDo == null) return NotFound();

        _context.Todos.Remove(toDo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ToDoExists(Guid id)
    {
        return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}