using Microsoft.Data.SqlClient;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories;

public class CategoryRepository
{
    private readonly ToDoContext _context;
    public CategoryRepository(ToDoContext context)
    {
        _context = context;
    }
    public List<CategoryModel> GetAllCategories()
    {
        return _context.Categories.ToList();
    }
}