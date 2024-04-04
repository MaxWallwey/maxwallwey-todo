using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Domain.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDocumentMessageDispatcher _dispatcher;
    private readonly IOfflineDispatcher _offlineDispatcher;
    private readonly ILogger<ToDoDocument> _logger;
    private readonly IRetryableExceptionFilter _retryableExceptionFilter;
    
    private readonly ISet<DocumentBase> _identityMap 
        = new HashSet<DocumentBase>(DocumentBaseEqualityComparer.Instance);

    public UnitOfWork(IDocumentMessageDispatcher dispatcher, 
        IOfflineDispatcher offlineDispatcher, 
        ILogger<ToDoDocument> logger, IRetryableExceptionFilter retryableExceptionFilter)
    {
        _dispatcher = dispatcher;
        _offlineDispatcher = offlineDispatcher;
        _logger = logger;
        _retryableExceptionFilter = retryableExceptionFilter;
    }
    
    public T? Find<T>(ObjectId id) where T : DocumentBase => 
        _identityMap
            .OfType<T>()
            .FirstOrDefault(ab => ab.Id == id);

    public void Register(DocumentBase document)
    {
        if (document != null)
        {
            _identityMap.Add(document);
        }
    }

    public void Register(IEnumerable<DocumentBase> aggregates)
    {
        foreach (var aggregate in aggregates)
        {
            Register(aggregate);
        }
    }

    public async Task Complete()
    {
        var toSkip = new HashSet<DocumentBase>();

        while (TryGetDocumentWithOutboxMessages(toSkip, out var document))
        {
            _logger.LogDebug("OUTBOX: Dispatching outbox messages for document {DocumentType} with ID '{DocumentId}'.",
                document.GetType().Name,
                document.Id.ToString());
            try
            {
                await _dispatcher.Dispatch(document);
            }
            catch (Exception exception)
            {
                toSkip.Add(document);
                if (_retryableExceptionFilter.IsRetryable(exception))
                {
                    var exceptionToLog = exception;
                    if (exception is AggregateException aggregateException)
                    {
                        if (aggregateException.InnerExceptions.Count == 1)
                        {
                            exceptionToLog = aggregateException.InnerExceptions[0];
                        }
                    }
                    _logger.LogWarning(exceptionToLog, "OUTBOX: Failed to dispatch outbox message with retryable exception for {DocumentType} with ID '{DocumentId}'.",
                        document.GetType().Name,
                        document.Id.ToString());
                }
                else
                {
                    var exceptionToLog = exception;
                    if (exception is AggregateException aggregateException)
                    {
                        if (aggregateException.InnerExceptions.Count == 1)
                        {
                            exceptionToLog = aggregateException.InnerExceptions[0];
                        }
                    }
                    _logger.LogError(exceptionToLog, "OUTBOX: Failed to dispatch outbox message with fatal exception for {DocumentType} with ID '{DocumentId}'.",
                        document.GetType().Name,
                        document.Id.ToString());
                }
                if (_retryableExceptionFilter.IsRetryable(exception))
                {
                    await _offlineDispatcher.DispatchOffline(document);
                }
            }
        }
    }
    
    private bool TryGetDocumentWithOutboxMessages(
        HashSet<DocumentBase> toSkip,
        [NotNullWhen(true)] out DocumentBase? document)
    {
        document = _identityMap
            .Except(toSkip, DocumentBaseEqualityComparer.Instance)
            .FirstOrDefault(x => x.Outbox.Any());
        return document != null;
    }

    public void Reset()
    {
        _identityMap.Clear();
    }
}