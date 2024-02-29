using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain.Todo;
using Todo.Api.Models;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseData<ToDoDocument>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("todo.findOne")]
    public async Task<ResponseData<ToDoDocument>> FindOne(Guid id)
    {
        try
        {
            return await Mediator.Send(new FindOneToDo.FindOneToDoRequest(id));
        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message);
        }
    }
    
    public FindOneController(IMediator mediator) : base(mediator)
    {
    }
}