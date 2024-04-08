using MongoDB.Bson;
using Todo.Api.Domain.Infrastructure;

namespace Todo.Api.Domain.Commands;

public class ProcessDocumentMessages
{
    public ObjectId DocumentId { get; set; }
    public string DocumentType { get; set; }

    // For NSB
    public ProcessDocumentMessages() { }

    private ProcessDocumentMessages(ObjectId documentId, string documentType)
    {
        DocumentId = documentId;
        DocumentType = documentType;
    }

    public static ProcessDocumentMessages New<TDocument>(
        TDocument document)
        where TDocument : DocumentBase
    {
        return new ProcessDocumentMessages(
            document.Id, 
            document.GetType().AssemblyQualifiedName);
    }
    public static ProcessDocumentMessages New<TDocument>(
        dynamic document)
        where TDocument : DocumentBase
    {
        return new ProcessDocumentMessages(
            ObjectId.Parse(document.Id), 
            typeof(TDocument).AssemblyQualifiedName);
    }
}