using Todo.Cli.Menu.Actions;
using FluentAssertions;

namespace Todo.Cli.Tests;

public class ToDoTests
{
    [Fact]
    public void ToDoTest()
    {
        var item = new ToDo("mock");

        item.Name.Should().Be("mock").And.NotBeNull();

        item.Id.Should().NotBe(Guid.Empty);

        item.IsComplete.Should().BeFalse();

        item.CreatedAt.Should().NotBe(DateTime.MinValue);
    }

    [Fact]
    public void Complete_CompleteAToDo_CompletesAToDo()
    {
        var item = new ToDo("mock");
        
        item.Complete();
        
        item.IsComplete.Should().BeTrue();
    }
}