namespace Todo.Api.Slices;

public class RouteTemplate
{
    public const string Error = "/error";

    public static class Todo
    {
        public const string Add = "/todo.add";
        public const string Complete = "/todo.complete";
        public const string FindMany = "/todo.findMany";
        public const string Remove = "/todo.remove";
    }
}