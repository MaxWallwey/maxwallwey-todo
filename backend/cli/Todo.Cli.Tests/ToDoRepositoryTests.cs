using FluentAssertions;

namespace Todo.Cli.Tests;

public class ToDoRepositoryTests
{
    [Fact]
    public void AddTaskTest()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.AddTask("mock");

        repository.Items.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void DeleteTaskTest()
    {
        ToDoRepository repository = new ToDoRepository();
        repository.Items.Add(new ToDo("mock"));
        
        repository.RemoveTask("mock");

        repository.Items.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CompleteTaskTest()
    {
        ToDoRepository repository = new ToDoRepository();
        repository.Items.Add(new ToDo("mock"));
        
        repository.CompleteTask("mock");

        repository.Items[0].IsComplete.Should().BeTrue();
    }

    [Fact]
    public void ListTasksTests()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.Items.Add(new ToDo("mock1"));
        
        repository.Items.Add(new ToDo("mock2", true));

        repository.ListIncompleteTasks()[0].IsComplete.Should().BeFalse();

        repository.ListCompleteTasks()[0].IsComplete.Should().BeTrue();
    }
}