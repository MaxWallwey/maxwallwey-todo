using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Todo.Api.Authentication;
using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Slices.Todo;

public class CompleteToDo
{
    public record Request(ObjectId Id) : IRequest<Response>;
    
    public record Response;
    
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator(IDocumentRepository<ToDoDocument> toDoRepository)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Please provide an ID for the todo.")
                .MustAsync(async (id, _) =>
                {
                    var todo = await toDoRepository.FindOneToDoAsync(id);
                    return todo != null;
                })
                .WithMessage("Todo was not found.");
        }
    }

    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IDocumentRepository<ToDoDocument> _documentRepository;
        private readonly IUserProfileAccessor _userProfileAccessor;

        public RequestHandler(IDocumentRepository<ToDoDocument> documentRepository, IUserProfileAccessor userProfileAccessor)
        {
            _documentRepository = documentRepository;
            _userProfileAccessor = userProfileAccessor;
        }
    
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var todo = await _documentRepository.FindOneToDoAsync(request.Id);
            
            var userId = _userProfileAccessor.Subject;

            if (todo!.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have access to this todo");
            }
            
            todo!.Complete();
            
            await _documentRepository.UpdateToDoAsync(todo);
            
            return new Response();
        }
    }
}