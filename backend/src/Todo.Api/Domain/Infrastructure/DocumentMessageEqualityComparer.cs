namespace Todo.Api.Domain.Infrastructure;

public class DocumentMessageEqualityComparer : IEqualityComparer<IDocumentMessage>
{
    public static readonly DocumentMessageEqualityComparer Instance = new();

    public bool Equals(IDocumentMessage? x, IDocumentMessage? y)
    {
        return x?.Id == y?.Id;
    }

    public int GetHashCode(IDocumentMessage obj)
    {
        return obj.Id.GetHashCode();
    }
}