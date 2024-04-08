using MongoDB.Bson;
using Newtonsoft.Json;
using Todo.Api.Sdk;

namespace Todo.Api.Domain.Infrastructure;

public abstract class DocumentBase : IDocument
{
    [JsonProperty(PropertyName = "id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    private HashSet<IDocumentMessage> _outbox
        = new(DocumentMessageEqualityComparer.Instance);
    private HashSet<IDocumentMessage> _inbox
        = new(DocumentMessageEqualityComparer.Instance);

    public IEnumerable<IDocumentMessage> Outbox
    {
        get => _outbox;
        protected set => _outbox = value == null
            ? new HashSet<IDocumentMessage>(DocumentMessageEqualityComparer.Instance)
            : new HashSet<IDocumentMessage>(value, DocumentMessageEqualityComparer.Instance);
    }

    public IEnumerable<IDocumentMessage> Inbox
    {
        get => _inbox;
        protected set => _inbox = value == null
            ? new HashSet<IDocumentMessage>(DocumentMessageEqualityComparer.Instance)
            : new HashSet<IDocumentMessage>(value, DocumentMessageEqualityComparer.Instance);
    }

    protected void Send(
        IDocumentMessage documentMessage) 
        => _outbox.Add(documentMessage);

    protected bool Receive<TDocumentMessage>(
        TDocumentMessage documentMessage)
        where TDocumentMessage : IDocumentMessage
    {
        if (_inbox.Contains(documentMessage))
        {
            return false;
        }

        _inbox.Add(documentMessage);

        return true;
    }

    public void Complete(
        IDocumentMessage documentMessage)
    {
        _outbox?.Remove(documentMessage);
    }
}