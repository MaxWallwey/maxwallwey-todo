namespace Todo.Api.Infrastructure;

public abstract class DocumentBase : IDocument
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid Id { get; set; } = Guid.NewGuid();
}