using Dapper;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoContext _context;
        private const string GET_ALL_CATEGORIES = "SELECT Id, Name FROM Categories";

        public CategoryRepository(ToDoContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetAllCategories()
        {
            using var connection = _context.CreateConnection();
            return connection.Query<CategoryModel>(GET_ALL_CATEGORIES).ToList();
        }
    }
}