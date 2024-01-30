using FluentAssertions;

namespace Todo.Cli.Tests;

public class ToDoRepositoryTests
{
    [Fact]
    public void AddTaskShouldAddANewToDo()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.AddTask("mock");

        repository.Items.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void DeleteTaskShouldDeleteAToDo()
    {
        ToDoRepository repository = new ToDoRepository();
        repository.Items.Add(new ToDo("mock"));
        
        repository.RemoveTask("mock");

        repository.Items.Should().BeNullOrEmpty();
    }

    [Fact]
    public void CompleteTaskShouldCompleteAToDo()
    {
        ToDoRepository repository = new ToDoRepository();
        repository.Items.Add(new ToDo("mock"));
        
        repository.CompleteTask("mock");

        repository.Items[0].IsComplete.Should().BeTrue();
    }

    [Fact]
    public void ListTasksTestsShouldListRespectiveToDos()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.Items.Add(new ToDo("mock1"));
        
        repository.Items.Add(new ToDo("mock2", true));

        repository.ListIncompleteTasks()[0].IsComplete.Should().BeFalse();

        repository.ListCompleteTasks()[0].IsComplete.Should().BeTrue();
    }
}