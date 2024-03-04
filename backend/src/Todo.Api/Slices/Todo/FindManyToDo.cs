using MediatR;
using Todo.Api.Domain;

namespace Todo.Api.Slices.Todo;

public abstract class FindManyToDo
{
    public record FindManyToDoRequest(bool? IsComplete) : IRequest<Response>;

    public record Response(List<ToDo> Data);
    public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, Response>
    {
        private readonly IToDoRepository _toDoRepository;

        public FindManyToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public async Task<Response> Handle(FindManyToDoRequest request, CancellationToken cancellationToken)
        {
            var todos = await _toDoRepository.FindManyAsync(request.IsComplete);

            return new Response(todos!);
        }
    }
}