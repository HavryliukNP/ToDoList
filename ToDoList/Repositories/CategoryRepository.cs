using Dapper;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoContext _context;

        public CategoryRepository(ToDoContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetAllCategories()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                string query = "SELECT Id, Name FROM Categories";
                return db.Query<CategoryModel>(query).AsList();
            }
        }
    }
}