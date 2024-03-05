using FluentAssertions;
using MongoDB.Bson;
using Todo.Api.Domain.Todo;

namespace Todo.Api.Tests.UnitTests;

public class ToDoDocumentTests
{
    [Fact]
    public void ToDoTest()
    {
        var item = new ToDoDocument("mock");

        item.Name.Should().Be("mock");

        item.Id.Should().NotBe(ObjectId.Empty);

        item.IsComplete.Should().BeFalse();

        item.CreatedAt.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public void Complete_CompletesSpecifiedToDo()
    {
        var item = new ToDoDocument("mock");
        
        item.Complete();
        
        item.IsComplete.Should().BeTrue();
    }
}