namespace Todo.Api.Domain.Infrastructure;

public interface IMailSender
{
    public void SendEmail(string recipientAddress, string subject, string body);
}