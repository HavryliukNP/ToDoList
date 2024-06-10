using Dapper;
using System.Data;
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
            using (IDbConnection db = _context.CreateConnection())
            {
                string query = "INSERT INTO Tasks (Title, Description, DueDate, CategoryId, IsCompleted) VALUES (@Title, @Description, @DueDate, @CategoryId, @IsCompleted)";
                db.Execute(query, task);
            }
        }

        public List<TaskModel> GetAllTasks()
        {
            var connection = _context.CreateConnection();
                
            var sql = "SELECT Id, Title, Description, DueDate, CategoryId, IsCompleted FROM Tasks ORDER BY IsCompleted";
            
            var tasks =  connection.Query<TaskModel>(sql);

            return tasks.ToList();
        }

        public void UpdateTaskStatus(int taskId, bool isCompleted)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                string query = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @TaskId";
                db.Execute(query, new { IsCompleted = isCompleted, TaskId = taskId });
            }
        }
    }
}