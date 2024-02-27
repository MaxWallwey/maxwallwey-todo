using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;

namespace Todo.Api.Slices;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IToDoRepository ToDoRepository;

    protected BaseController(IToDoRepository toDoRepository)
    {
        ToDoRepository = toDoRepository;
    }

    //public IMediator Mediator { get; set; }
}