using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class FindOneController : BaseController
{
    // List ToDo based on ID
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseData<ToDo>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("todo.findOne")]
    public async Task<ResponseData<ToDo>> FindOne(Guid id)
    {
        return await Mediator.Send(new FindOneToDo.FindOneToDoRequest(id));
    }
    
    public FindOneController(IMediator mediator) : base(mediator)
    {
    }
}