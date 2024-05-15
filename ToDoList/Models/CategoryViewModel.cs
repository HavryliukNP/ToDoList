namespace ToDoList.Models;

public class CategoryViewModel
{
    public Dictionary<int, string> Categories { get; set; }
    public List<TaskModel> Tasks { get; set; }
}