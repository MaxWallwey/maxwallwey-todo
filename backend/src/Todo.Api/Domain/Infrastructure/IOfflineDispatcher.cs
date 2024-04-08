namespace Todo.Api.Domain.Infrastructure;

public interface IOfflineDispatcher
{
    Task DispatchOffline(DocumentBase document);
}