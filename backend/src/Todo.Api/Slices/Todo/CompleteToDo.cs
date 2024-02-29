using MediatR;
using Todo.Api.Domain;
using Todo.Api.Models;

namespace Todo.Api.Slices;

public class CompleteToDo
{
    public record CompleteToDoRequest(Guid Id) : IRequest;

    public class CompleteToDoHandler : IRequestHandler<CompleteToDoRequest>
    {
        private readonly IToDoRepository _toDoRepository;

        public CompleteToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public Task Handle(CompleteToDoRequest request, CancellationToken cancellationToken)
        {
            return _toDoRepository.CompleteToDoAsync(request.Id);
        }
    }
}