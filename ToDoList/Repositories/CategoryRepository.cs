using System.Collections.Generic;
using System.Linq;
using Dapper;
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
            var connection = _context.CreateConnection();
            
            var sql = "SELECT Id, Name FROM Categories";
            
            var categories = connection.Query<CategoryModel>(sql);

            return categories.ToList();
        }
    }
}