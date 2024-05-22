using Dapper;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class CategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CategoryModel> GetAllCategories()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name FROM Categories";
                return db.Query<CategoryModel>(query).AsList();
            }
        }
    }
}