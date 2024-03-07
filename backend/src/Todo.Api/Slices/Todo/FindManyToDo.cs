using MediatR;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public abstract class FindManyToDo
{
    public record FindManyToDoRequest(bool? IsComplete) : IRequest<Response>;

    public record Response(List<ToDoDocument> Data);
    public class FindManyToDoHandler : IRequestHandler<FindManyToDoRequest, Response>
    {
        private readonly IDocumentRepository<ToDoDocument> _toDoRepository;

        public FindManyToDoHandler(IDocumentRepository<ToDoDocument> toDoRepository)
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