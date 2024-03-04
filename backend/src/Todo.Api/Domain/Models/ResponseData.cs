namespace Todo.Api.Domain.Models;

public class ResponseData<T>
{
    public ResponseData(T data)
    {
        Data = data;
    }

    public T? Data { get; }
}
