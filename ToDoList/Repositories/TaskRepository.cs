using Dapper;
using System.Data;
using System.Data.SqlClient; 
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskModel task)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tasks (Title, Description, DueDate, CategoryId, IsCompleted) VALUES (@Title, @Description, @DueDate, @CategoryId, @IsCompleted)";
                db.Execute(query, task);
            }
        }
        
        public List<TaskModel> GetAllTasks()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Title, Description, DueDate, CategoryId, IsCompleted FROM Tasks ORDER BY IsCompleted";
                return db.Query<TaskModel>(query).AsList();
            }
        }

        public void UpdateTaskStatus(int taskId, bool isCompleted)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @TaskId"; 
                db.Execute(query, new { IsCompleted = isCompleted, TaskId = taskId });
            }
        }
    }
}