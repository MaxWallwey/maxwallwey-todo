namespace Todo.Api.Authentication;

public interface IUserProfileAccessor
{
    public string? Subject { get; }
}