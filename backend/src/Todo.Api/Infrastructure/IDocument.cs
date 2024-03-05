namespace Todo.Api.Infrastructure;

public interface IDocument
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
}