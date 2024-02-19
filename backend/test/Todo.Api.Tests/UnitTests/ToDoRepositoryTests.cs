using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using ToDo.API.SDK;


namespace Todo.Cli.Tests.UnitTests;
public class ToDoRepositoryTests
{
    // How do I run these tests? Connection fails. Do I need to create a WebApplicationFactory (like in the IntegrationTests)
    [Fact]
    public async Task AddTask_AddsNewToDo()
    {
        var mockRepository = Substitute.For<ToDoRepository>();
        
        var newTaskId = await mockRepository.AddTask("mock");

        mockRepository.ListToDoFromTask(newTaskId).Should().NotBeNull();
    }

    [Fact]
    public async Task RemoveTask_DeletesTask()
    {
        var mockRepository = Substitute.For<ToDoRepository>();
        var newTaskId = await mockRepository.AddTask("mock");

        mockRepository.RemoveTask(newTaskId);

        mockRepository.ListToDoFromTask(newTaskId).Should().BeNull();
    }

    [Fact]
    public async Task CompleteTask_ChangeCompleteStatusToComplete()
    {
        var mockRepository = Substitute.For<ToDoRepository>();
        var newTaskId = await mockRepository.AddTask("mock");

        mockRepository.CompleteTask(newTaskId);

        mockRepository.ListToDoFromTask(newTaskId).Result.IsComplete.Should().BeTrue();
    }

    [Fact]
    public async Task ListIncompleteTasks_ListsAllIncompleteTasks()
    {
        var mockRepository = Substitute.For<ToDoRepository>();
        await mockRepository.AddTask("mock0");
        var newTaskId1 = await mockRepository.AddTask("mock1");
        mockRepository.CompleteTask(newTaskId1);

        var incompleteTasks = mockRepository.ListIncompleteTasks().Result;

        incompleteTasks.Data.Should().AllSatisfy(x => x.IsComplete = false);
    }
    
    [Fact]
    public async Task ListCompleteTasks_ListsAllCompleteTasks()
    {
        var mockRepository = Substitute.For<ToDoRepository>();
        await mockRepository.AddTask("mock0");
        var newTaskId1 = await mockRepository.AddTask("mock1");
        mockRepository.CompleteTask(newTaskId1);

        var completeTasks = mockRepository.ListCompleteTasks().Result;

        completeTasks.Data.Should().AllSatisfy(x => x.IsComplete = true);
    }
}