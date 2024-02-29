using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;
using Todo.Api.Slices;

namespace Todo.Api.Controllers;

[ApiController]
public class FindManyController : BaseController
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseData<List<ToDo>>))]
    [HttpGet("todo.findMany")]
    public async Task<ResponseData<List<ToDo>>> FindMany(bool isComplete)
    {
        return await Mediator.Send(new FindManyToDo.FindManyToDoRequest(isComplete));
    }
    
    public FindManyController(IMediator mediator) : base(mediator)
    {
    }
}