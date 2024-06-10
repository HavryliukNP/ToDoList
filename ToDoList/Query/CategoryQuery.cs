using GraphQL.Types;
using ToDoList.Repositories;
using ToDoList.Type;

namespace ToDoList.Query;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery(ICategoryRepository categoryRepository)
    {
        Field<ListGraphType<CategoryType>>("categories").Resolve(context =>
        {
            return categoryRepository.GetAllCategories();
        });
    }
}