using MediatR;

namespace Todo.Api.Models;

public class ResponseData<T>
{
    public ResponseData(T data)
    {
        Data = data;
    }

    public T Data { get; }
}
