using FluentAssertions;

namespace Todo.Cli.Tests;

public class ToDoRepositoryTests
{
    [Fact]
    public void AddTask_AddNewTask_AddsNewToDoToDB()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.AddTask("mock");

        repository.Items.Should().SatisfyRespectively(
            first =>
            {
                first.Name.Should().NotBeNull();
            },
            second =>
            {
                second.Name.Should().BeNullOrEmpty();
            });
    }

    [Fact]
    public void RemoveTask_DeleteTask_DeletesTaskFromDB()
    {
        ToDoRepository repository = new ToDoRepository(new List<ToDo>{new ToDo("mock")});
        
        repository.RemoveTask("mock");

        repository.Items.Count.Should().Be(0);
    }

    [Fact]
    public void CompleteTask_CompleteATask_ChangeCompleteStatusToComplete()
    {
        ToDoRepository repository = new ToDoRepository();
        repository.Items.Add(new ToDo("mock"));
        
        repository.CompleteTask("mock");

        repository.Items.Should().SatisfyRespectively(
            first => first.IsComplete.Should().BeTrue()
        );
    }

    [Fact]
    public void ListIncompleteTasks_ListTasks_ListsAllIncompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.Items.Add(new ToDo("mock1"));
        
        repository.Items.Add(new ToDo("mock2", true));

        repository.ListIncompleteTasks()[0].IsComplete.Should().BeFalse();
    }
    
    [Fact]
    public void ListCompleteTasks_ListTasks_ListsAllCompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository();
        
        repository.Items.Add(new ToDo("mock1"));
        
        repository.Items.Add(new ToDo("mock2", true));

        repository.ListCompleteTasks()[0].IsComplete.Should().BeTrue();
    }
}