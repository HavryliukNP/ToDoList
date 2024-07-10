using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Repositories;

public interface ITaskRepository
{
    List<TaskModel> GetAllTasks();
    void AddTask(TaskModel task);
    void UpdateTaskStatus(int taskId, bool isCompleted);
    void DeleteTask(int taskId);
}