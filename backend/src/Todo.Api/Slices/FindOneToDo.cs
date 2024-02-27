using MediatR;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public record FindOneToDoRequest(Guid Id) : IRequest<ResponseData<ToDo>>;

public class FindOneToDoHandler : IRequestHandler<FindOneToDoRequest, ResponseData<ToDo>>
{
    private readonly IToDoRepository _toDoRepository;

    public FindOneToDoHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }
    
    public Task<ResponseData<ToDo>> Handle(FindOneToDoRequest request, CancellationToken cancellationToken)
    {
        return _toDoRepository.FindOneToDoAsync(request.Id);
    }
}