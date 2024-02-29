namespace Todo.Api.Infrastructure;

public interface IDocument
{
    public DateTime CreatedAt { get; }
    public Guid Id { get; }
}