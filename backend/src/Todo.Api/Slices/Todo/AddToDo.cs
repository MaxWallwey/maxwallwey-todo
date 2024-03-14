using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Todo.Api.Authentication;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public abstract class AddToDo
{
    public record AddToDoRequest(string Name) : IRequest<Response>;
    
    public record Response(ObjectId Data);
    
    public class CreateToDoValidator : AbstractValidator<AddToDoRequest>
    {
        public CreateToDoValidator(IDocumentRepository<ToDoDocument> toDoRepository)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please provide a name for the todo.")
                .MustAsync(async (name, _) => name != null && !await toDoRepository.AnyAsync(name))
                .WithMessage("Name must be unique.");
        }
    }

    public class AddToDoHandler : IRequestHandler<AddToDoRequest, Response>
    {
        private readonly IDocumentRepository<ToDoDocument> _toDoRepository;
        private readonly IUserProfileAccessor _userProfileAccessor;

        public AddToDoHandler(IDocumentRepository<ToDoDocument> toDoRepository, IUserProfileAccessor userProfileAccessor)
        {
            _toDoRepository = toDoRepository;
            _userProfileAccessor = userProfileAccessor;
        }
        public async Task<Response> Handle(AddToDoRequest request, CancellationToken cancellationToken)
        {
            var userId = _userProfileAccessor.Subject;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("Must be logged in to add a todo");
            }
            
            var todo = new ToDoDocument(_userProfileAccessor.Subject!, request.Name);
            await _toDoRepository.AddToDoAsync(todo);
            return new Response(todo.Id);
        }
    }   
}