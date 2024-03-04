using MediatR;
using Todo.Api.Domain;
using Todo.Api.Domain.Models;

namespace Todo.Api.Slices.Todo;

public abstract class FindManyToDo
{
    public record FindManyToDoRequest(bool? IsComplete) : IRequest<ResponseData<List<ToDo>>?>;

    public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, ResponseData<List<ToDo>>?>
    {
        private readonly IToDoRepository _toDoRepository;

        public FindManyToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public Task<ResponseData<List<ToDo>>?> Handle(FindManyToDoRequest request, CancellationToken cancellationToken)
        {
            return _toDoRepository.FindManyAsync(request.IsComplete);
        }
    }
}