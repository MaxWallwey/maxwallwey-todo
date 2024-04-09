using System.ComponentModel.DataAnnotations;
using Todo.Api.Domain.Collections;
using Todo.Api.Domain.Infrastructure;

namespace Todo.Api.Domain.Todo;

public class ToDoDocument : DocumentBase
{
    public ToDoDocument(string userId, string name)
    {
        Name = name;
        IsComplete = false;
        UserId = userId;
    }
    
    public string Name { get; set; }
    public bool IsComplete { get; private set; }
    public string UserId { get; private set; }

    public void Complete()
    {
        IsComplete = true;
        
        //SendTodoCompletedEmail();
    }

    private void SendTodoCompletedEmail()
    {
        var mailSender = new MailSender();
        
        mailSender.SendEmail("max.wallwey@gmail.com", $"TODO - {Name}", $"Your todo '{Name}' has been completed.");
    }

    public bool Handle(SendTodoCompletedEmail message) => Receive(message);
}