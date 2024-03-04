using MediatR;
using Todo.Api.Domain.Todo;
using Todo.Api.Infrastructure;

namespace Todo.Api.Slices.Todo;

public abstract class FindManyToDo
{
    public record FindManyToDoRequest(bool? IsComplete) : IRequest<Response>;

    public record Response(List<ToDoDocument> Data);
    public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, Response>
    {
        private readonly IDocumentRepository _toDoRepository;

        public FindManyToDoHandler(IDocumentRepository toDoRepository)
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