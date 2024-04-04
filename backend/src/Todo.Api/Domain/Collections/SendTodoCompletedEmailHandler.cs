using Todo.Api.Domain.Infrastructure;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Domain.Collections;

public class SendTodoCompletedEmailHandler : IDocumentMessageHandler<SendTodoCompletedEmail>
{
    private readonly IMailSender _mailSender;
    private readonly IDocumentRepository<ToDoDocument> _documentRepository;
    private readonly ILogger<SendTodoCompletedEmailHandler> _logger;

    public SendTodoCompletedEmailHandler(IMailSender mailSender, 
        IDocumentRepository<ToDoDocument> documentRepository, 
        ILogger<SendTodoCompletedEmailHandler> logger)
    {
        _mailSender = mailSender;
        _documentRepository = documentRepository;
        _logger = logger;
    }
    
    public async Task Handle(SendTodoCompletedEmail message)
    {
        var todo = await _documentRepository.FindOneToDoAsync(message.Id);

        if (todo == null)
        {
            throw new Exception();
        }
        if (!todo.Handle(message))
        {
            return;
        }

        try
        {
            _mailSender.SendEmail("max.wallwey@gmail.com", $"Todo has been completed",
                "Well done, one of your todos has been completed!");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to send todo completed email.");
        }
    }
}