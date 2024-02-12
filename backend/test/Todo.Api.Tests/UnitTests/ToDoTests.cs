using FluentAssertions;

namespace Todo.Cli.Tests.UnitTests;

public class ToDoTests
{
    [Fact]
    public void ToDoTest()
    {
        var item = new API.Domain.ToDo("mock");

        item.Name.Should().Be("mock");

        item.Id.Should().NotBe(Guid.Empty);

        item.IsComplete.Should().BeFalse();

        item.CreatedAt.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public void Complete_CompletesSpecifiedToDo()
    {
        var item = new API.Domain.ToDo("mock");
        
        item.Complete();
        
        item.IsComplete.Should().BeTrue();
    }
}