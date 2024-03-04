using MediatR;
using Todo.Api.Domain;
using Todo.Api.Domain.Models;

namespace Todo.Api.Slices.Todo;

public abstract class FindOneToDo
{
    public record FindOneToDoRequest(Guid Id) : IRequest<ResponseData<ToDo?>>;

    public class FindOneToDoHandler : IRequestHandler<FindOneToDoRequest, ResponseData<ToDo?>>
    {
        private readonly IToDoRepository _toDoRepository;

        public FindOneToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public Task<ResponseData<ToDo?>> Handle(FindOneToDoRequest request, CancellationToken cancellationToken)
        {
            var todo = _toDoRepository.FindOneToDoAsync(request.Id);

            return todo!;
        }
    }
}