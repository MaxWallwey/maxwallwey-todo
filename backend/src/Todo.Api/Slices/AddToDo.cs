using MediatR;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public record AddToDoRequest(CreateToDo Model) : IRequest<ResponseData<Guid>>;

public class AddToDoHandler : IRequestHandler<AddToDoRequest, ResponseData<Guid>>
{
    private readonly IToDoRepository _toDoRepository;

    public AddToDoHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }
    
    public Task<ResponseData<Guid>> Handle(AddToDoRequest request, CancellationToken cancellationToken)
    {
        return _toDoRepository.AddToDoAsync(request.Model);
    }
}