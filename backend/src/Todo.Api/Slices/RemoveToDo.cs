using MediatR;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public class RemoveToDo
{
    public record RemoveToDoRequest(Guid Id) : IRequest;

    public class RemoveToDoHandler : IRequestHandler<RemoveToDoRequest>
    {
        private readonly IToDoRepository _toDoRepository;

        public RemoveToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public Task Handle(RemoveToDoRequest request, CancellationToken cancellationToken)
        {
            return _toDoRepository.RemoveToDoAsync(request.Id);
        }
    }
}