using FluentAssertions;
using Refit;


namespace Todo.Cli.Tests.UnitTests;
public class ToDoRepositoryTests
{
    
    // How do I run these tests? Connection fails. Do I need to create a WebApplicationFactory (like in the IntegrationTests)
    [Fact]
    public async Task AddTask_AddsNewToDo()
    {
        ToDoRepository repository = new ToDoRepository();
        var newTaskId = await repository.AddTask("mock");

        repository.ListToDoFromTask(newTaskId).Should().NotBeNull();
    }

    [Fact]
    public async Task RemoveTask_DeletesTask()
    {
        ToDoRepository repository = new ToDoRepository();
        var newTaskId = await repository.AddTask("mock");

        repository.RemoveTask(newTaskId);

        repository.ListToDoFromTask(newTaskId).Should().BeNull();
    }

    [Fact]
    public async Task CompleteTask_ChangeCompleteStatusToComplete()
    {
        ToDoRepository repository = new ToDoRepository();
        var newTaskId = await repository.AddTask("mock");

        repository.CompleteTask(newTaskId);
        
        repository.ListToDoFromTask(newTaskId).Result.IsComplete.Should().BeTrue();
    }

    [Fact]
    public async Task ListIncompleteTasks_ListsAllIncompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository();
        await repository.AddTask("mock0");
        var newTaskId1 = await repository.AddTask("mock1");
        repository.CompleteTask(newTaskId1);

        var incompleteTasks = repository.ListIncompleteTasks().Result;

        incompleteTasks.Data.Should().AllSatisfy(x => x.IsComplete = false);
    }
    
    [Fact]
    public async Task ListCompleteTasks_ListsAllCompleteTasks()
    {
        ToDoRepository repository = new ToDoRepository();
        await repository.AddTask("mock0");
        var newTaskId1 = await repository.AddTask("mock1");
        repository.CompleteTask(newTaskId1);

        var completeTasks = repository.ListCompleteTasks().Result;

        completeTasks.Data.Should().AllSatisfy(x => x.IsComplete = true);
    }
}