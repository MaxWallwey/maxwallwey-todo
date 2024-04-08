using MongoDB.Bson;

namespace Todo.Api.Domain.Infrastructure;

public interface IUnitOfWork
{
    T? Find<T>(ObjectId id) where T : DocumentBase;
    void Register(DocumentBase document);
    void Register(IEnumerable<DocumentBase> aggregates);
    Task Complete(CancellationToken cancellationToken);
    void Reset();
}