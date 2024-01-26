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

        item.Id.Should().NotBeEmpty();

        item.IsComplete.Should().BeFalse();

        item.CreatedAt.Should().BeOnOrAfter(DateTime.Today);
    }

    [Fact]
    public void ToDoCompleteTest()
    {
        var item = new ToDo("mock");
        
        item.Complete();
        
        item.IsComplete.Should().BeTrue();
    }
}