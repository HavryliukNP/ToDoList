using ToDoList.Models;

namespace ToDoList.Repositories;

public interface ICategoryRepository
{
    List<CategoryModel> GetAllCategories();
}