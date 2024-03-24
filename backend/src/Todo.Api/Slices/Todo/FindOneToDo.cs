using MediatR;
using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public abstract class FindOneToDo
{
    public record Request(ObjectId Id) : IRequest<Response>;
    
    public record Response(ToDoDocument Data);
    
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IDocumentRepository<ToDoDocument> _toDoRepository;

        public RequestHandler(IDocumentRepository<ToDoDocument> toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
    
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var todo = await _toDoRepository.FindOneToDoAsync(request.Id);

            return new Response(todo!);
        }
    }
}