using MediatR;
using Todo.Api.Authentication;
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
        private readonly IUserProfileAccessor _userProfileAccessor;

        public FindManyToDoHandler(IDocumentRepository<ToDoDocument> toDoRepository, IUserProfileAccessor userProfileAccessor)
        {
            _toDoRepository = toDoRepository;
            _userProfileAccessor = userProfileAccessor;
        }
    
        public async Task<Response> Handle(FindManyToDoRequest request, CancellationToken cancellationToken)
        {
            var userId = _userProfileAccessor.Subject;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("Must be logged in to view todos");
            }

            var collection = await _toDoRepository.FindManyAsync(request.IsComplete);
            
            var data = collection!.Where(t => t.UserId == userId)
                .ToList();
            
            return new Response(data);
        }
    }
}