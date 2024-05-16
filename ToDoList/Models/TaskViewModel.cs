namespace ToDoList.Models;

public class TaskViewModel
{
    public Dictionary<int, string> Categories { get; set; }
    public List<TaskModel> Tasks { get; set; }
}