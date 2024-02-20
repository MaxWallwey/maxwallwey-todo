using FluentAssertions;
using Microsoft.Extensions.DependencyModel;
using Moq;
using Todo.API.Controllers;
using ToDo.API.SDK;
using Todo.Cli.Menu.Actions;

namespace Todo.Cli.Tests.UnitTests;

public class ActionTests
{
    private Mock<IToDoClient> _mock = new();
    
    [Fact]
    public async Task AddTask_AddsNewToDo()
    {
        var model = new CreateToDo { Name = "test" };
        _mock.Setup(x => x.AddToDo(model)).ReturnsAsync(new ResponseData<Guid>(Guid.NewGuid()));
        var test = _mock.Object;

        var newTask = await test.AddToDo(model);

        newTask.Data.Should().NotBeEmpty();
    }

    [Fact]
    public async Task RemoveTask_DeletesTask()
    {
        var id = Guid.NewGuid();
        _mock.Setup(x => x.RemoveToDo(id));
        var test = _mock.Object;

        await test.RemoveToDo(id);
    }

    [Fact]
    public async Task CompleteTask_ChangeCompleteStatusToComplete()
    {
        var id = Guid.NewGuid();
        _mock.Setup(x => x.CompleteToDo(id));
        var test = _mock.Object;

        await test.CompleteToDo(id);
    }

    [Fact]
    public async Task ListIncompleteTasks_ListsAllIncompleteTasks()
    {
        _mock.Setup(x => x.FindMany(false)).ReturnsAsync(new ResponseData<List<ToDo.API.SDK.ToDo>>(new List<ToDo.API.SDK.ToDo>()));
        var test = _mock.Object;

        var incompleteTasks = await test.FindMany(false);

        incompleteTasks.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task ListCompleteTasks_ListsAllCompleteTasks()
    {
        _mock.Setup(x => x.FindMany(true)).ReturnsAsync(new ResponseData<List<ToDo.API.SDK.ToDo>>(new List<ToDo.API.SDK.ToDo>()));
        var test = _mock.Object;

        var incompleteTasks = await test.FindMany(true);

        incompleteTasks.Data.Should().NotBeNull();
    }
}