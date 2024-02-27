using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public record FindManyToDoRequest(bool IsComplete) : IRequest<ResponseData<List<ToDo>>>;

public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, ResponseData<List<ToDo>>>
{
    private readonly IToDoRepository _toDoRepository;

    public FindManyToDoHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }
    
    public Task<ResponseData<List<ToDo>>> Handle(FindManyToDoRequest request, CancellationToken cancellationToken)
    {
        return _toDoRepository.FindManyAsync(request.IsComplete);
    }
}

