using MongoDB.Bson;
using Todo.Api.Domain.Commands;

namespace Todo.Api.Domain.Infrastructure;

public interface IDocumentMessageDispatcher
{
    Task<Exception> Dispatch(
        DocumentBase document, CancellationToken cancellationToken);


    Task Dispatch(ProcessDocumentMessages command);
}