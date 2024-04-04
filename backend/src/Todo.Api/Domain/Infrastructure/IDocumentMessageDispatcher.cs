namespace Todo.Api.Domain.Infrastructure;

public interface IDocumentMessageDispatcher
{
    Task<Exception> Dispatch(
        DocumentBase document);

    Task Dispatch();
}