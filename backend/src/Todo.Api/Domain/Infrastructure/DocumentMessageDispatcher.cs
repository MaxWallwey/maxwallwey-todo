using MediatR;
using MongoDB.Bson;
using Todo.Api.Domain.Commands;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Domain.Infrastructure;

public class DocumentMessageDispatcher : IDocumentMessageDispatcher
{
    private readonly ServiceFactory _serviceFactory;

    public DocumentMessageDispatcher(ServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<Exception> Dispatch(DocumentBase document, CancellationToken cancellationToken)
    {
        var repository = GetRepository(document.GetType());
        foreach (var documentMessage in document.Outbox.ToArray())
        {
            try
            {
                var handler = GetHandler(documentMessage);

                await handler.Handle(documentMessage, _serviceFactory);

                document.Complete(documentMessage);

                await repository.Update(document);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        return null;
    }

    public async Task Dispatch(ProcessDocumentMessages command)
    {
        var documentType = Type.GetType(command.DocumentType);
        var repository = GetRepository(documentType);
        var document = await repository.FindById(command.DocumentId);

        if (document == null)
        {
            return;
        }

        foreach (var message in document.Outbox.ToArray())
        {
            var handler = GetHandler(message);

            await handler.Handle(message, _serviceFactory);

            document.Complete(message);

            await repository.Update(document);
        }
    }

    private static DomainEventDispatcherHandler GetHandler(
        IDocumentMessage documentMessage)
    {
        var genericDispatcherType = typeof(DomainEventDispatcherHandler<>)
            .MakeGenericType(documentMessage.GetType());

        return (DomainEventDispatcherHandler)
            Activator.CreateInstance(genericDispatcherType);
    }

    private DocumentDbRepo GetRepository(Type aggregateType)
    {
        var repoBaseType = typeof(DocumentDbRepo<>).MakeGenericType(aggregateType);
        var repoType = typeof(IDocumentRepository<>).MakeGenericType(aggregateType);
        var repoInstance = _serviceFactory(repoType);

        return (DocumentDbRepo)Activator.CreateInstance(repoBaseType, repoInstance);
    }

    private abstract class DocumentDbRepo
    {
        public Task<DocumentBase> FindById(ObjectId id)
        {
            return null;
        }

        public Task Update(DocumentBase document)
        {
            return null;
        }
    }

    private class DocumentDbRepo<T> : DocumentDbRepo
        where T : DocumentBase
    {
        private readonly IDocumentRepository<T> _repository;

        public DocumentDbRepo(IDocumentRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<DocumentBase> FindById(ObjectId id)
        {
            var root = await _repository.FindOneToDoAsync(id);

            return root;
        }

        public Task Update(ToDoDocument document)
        {
            return _repository.UpdateToDoAsync(document);
        }
    }

    private abstract class DomainEventDispatcherHandler
    {
        public abstract Task Handle(
            IDocumentMessage documentMessage,
            ServiceFactory factory);
    }

    private class DomainEventDispatcherHandler<T> : DomainEventDispatcherHandler
        where T : IDocumentMessage
    {
        public override Task Handle(IDocumentMessage documentMessage, ServiceFactory factory)
        {
            return HandleCore((T)documentMessage, factory);
        }

        private static async Task HandleCore(T domainEvent, ServiceFactory factory)
        {
            var handlers = factory.GetInstances<IDocumentMessageHandler<T>>();
            foreach (var handler in handlers)
            {
                await handler.Handle(domainEvent);
            }
        }
    }
}