using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories;

public class TaskRepository
{
    private readonly ToDoContext _context;

    public TaskRepository(ToDoContext context)
    {
        _context = context;
    }

    public void AddTask(TaskModel task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public List<TaskModel> GetAllTask()
    {
        return _context.Tasks.Include(t => t.Category).OrderBy(t => t.IsCompleted).ToList();
    }

    public void UpdateTaskStatus(int taskId, bool isCompleted)
    {
        var task = _context.Tasks.Find(taskId);
        if (task != null)
        {
            task.IsCompleted = isCompleted;
            _context.SaveChanges();
        }
    }
}