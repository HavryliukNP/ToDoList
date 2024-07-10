using System.Collections.Generic;
using System.Linq;
using Dapper;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoContext _context;

        public TaskRepository(ToDoContext context)
        {
            _context = context;
        }

        public void AddTask(TaskModel task)
        {
            var connection = _context.CreateConnection();
                
            var sql = "INSERT INTO Tasks (Title, Description, DueDate, CategoryId, IsCompleted) VALUES (@Title, @Description, @DueDate, @CategoryId, @IsCompleted)";
            
            connection.Execute(sql, task);
        }

        public List<TaskModel> GetAllTasks()
        {
            var connection = _context.CreateConnection();
                
            var sql = "SELECT Id, Title, Description, DueDate, CategoryId, IsCompleted FROM Tasks ORDER BY IsCompleted, Id DESC";
    
            var tasks = connection.Query<TaskModel>(sql);

            return tasks.ToList();
        }

        public void UpdateTaskStatus(int taskId, bool isCompleted)
        {
            var connection = _context.CreateConnection();
                
            var sql = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @TaskId";
            
            connection.Execute(sql, new { IsCompleted = isCompleted, TaskId = taskId });
        }
        public void DeleteTask(int taskId)
        {
            var connection = _context.CreateConnection();
                
            var sql = "DELETE FROM Tasks WHERE Id = @TaskId";
    
            connection.Execute(sql, new { TaskId = taskId });
        }

    }
}