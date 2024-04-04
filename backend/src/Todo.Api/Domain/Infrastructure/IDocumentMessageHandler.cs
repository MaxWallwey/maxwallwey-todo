namespace Todo.Api.Domain.Infrastructure;

public interface IDocumentMessageHandler<in T>
    where T : IDocumentMessage
{
    Task Handle(T message);
}