namespace Todo.Cli.Tests;

public class DomainTests
{
    [Fact]
    public void NewTaskHasId()
    {
        var item = new Todo("mock");

        Assert.True(item.Id != null);
    }
    
    // [Fact]
    // public void NewTaskHasGuid()
    // {
    //     var item = new Todo("mock");
    //
    //     Assert.IsType<Guid>(item.Id);
    // }

    [Fact]
    public void NewTaskHasFalseCompletionStatusWhenNotSpecified()
    {
        var item = new Todo("mock");
        
        Assert.False(item.IsComplete);
    }
    
    [Fact]
    public void DateTimeOfTask()
    {
        var item = new Todo("mock");
        
        Assert.Equal(DateTime.Now, item.CreatedAt);
    }

    [Fact]
    public void AddTask_AddsTaskToList()
    {
        ToDoRepository mock = new ToDoRepository();
                
        mock.AddTask("mock");
        
        
    }
}