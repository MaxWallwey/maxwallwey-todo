using MediatR;
using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public abstract class FindOneToDo
{
    public record FindOneToDoRequest(ObjectId Id) : IRequest<Response>;
    
    public record Response(ToDoDocument Data);
    
    public class FindOneToDoHandler : IRequestHandler<FindOneToDoRequest, Response>
    {
        private readonly IDocumentRepository<ToDoDocument> _toDoRepository;

        public FindOneToDoHandler(IDocumentRepository<ToDoDocument> toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public async Task<Response> Handle(FindOneToDoRequest request, CancellationToken cancellationToken)
        {
            var todo = await _toDoRepository.FindOneToDoAsync(request.Id);

            return new Response(todo!);
        }
    }
}