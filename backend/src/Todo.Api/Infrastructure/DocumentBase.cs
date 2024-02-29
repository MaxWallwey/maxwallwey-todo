using Todo.Api.Domain.Todo;

namespace Todo.Api.Infrastructure;

public abstract class DocumentBase : IDocument
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Guid Id { get; } = Guid.NewGuid();
}