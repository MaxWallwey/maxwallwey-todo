using System.Collections;
using Todo.Api.Slices;

namespace Todo.Api.Tests;

public class RouteTemplateCombinations
{
    private static readonly IReadOnlyList<string> s_routes;

    static RouteTemplateCombinations()
        => s_routes = GetNestedConstants(typeof(RouteTemplate))!;

    private static IReadOnlyList<string?> GetNestedConstants(Type type) =>
        type.GetFields()
            .Select(fieldInfo => fieldInfo.GetRawConstantValue())
            .Concat(type.GetNestedTypes().SelectMany(GetNestedConstants))
            .Select(x => x as string)
            .ToList();

    public class AllSliceRoutes : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator() => s_routes
            .Except(new[] { RouteTemplate.Error })
            .Select(route => new object?[] { route })
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}