using FluentAssertions;

namespace Todo.Cli.Tests;

public class ToDoRepositoryTests
{
    [Fact]
    public void AddTask_AddsNewToDo()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.AddTask("mock");

        repository.Items.Should().SatisfyRespectively(
            first => first.Name.Should().NotBeNull());
    }

    [Fact]
    public void RemoveTask_DeletesTask()
    {
        var todo1 = new ToDo("mock");

        ToDoRepository repository = new ToDoRepository(new List<ToDo>{ todo1 });

        repository.RemoveTask(todo1.Id);

        repository.Items.Count.Should().Be(0);
    }

    [Fact]
    public void CompleteTask_ChangeCompleteStatusToComplete()
    {
        var todo1 = new ToDo("mock");

        ToDoRepository repository = new ToDoRepository(new List<ToDo>{ todo1 });
        
        repository.CompleteTask(todo1.Id);

        repository.Items.Should().SatisfyRespectively(
            first => first.IsComplete.Should().BeTrue()
        );
    }

    [Fact]
    public void ListIncompleteTasks_ListsAllIncompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository(new List<ToDo>
        {
            new ToDo("mock1"), 
            new ToDo("mock2", true)
        });

        repository.ListIncompleteTasks()[0].IsComplete.Should().BeFalse();
    }
    
    [Fact]
    public void ListCompleteTasks_ListsAllCompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository(new List<ToDo>
        {
            new ToDo("mock1"), 
            new ToDo("mock2", true)
        });

        repository.ListCompleteTasks()[0].IsComplete.Should().BeTrue();
    }
}