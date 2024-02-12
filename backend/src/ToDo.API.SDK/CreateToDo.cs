using System.ComponentModel.DataAnnotations;

namespace ToDo.API.SDK;

public class CreateToDo
{ 
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Name should be minimum 3 characters and a maximum of 50 characters")]
    public string? Name { get; set; }
}