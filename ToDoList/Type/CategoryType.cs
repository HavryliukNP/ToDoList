using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Type;

public class CategoryType : ObjectGraphType<CategoryModel>
{
    public CategoryType()
    {
        Field(c => c.Id);
        Field(c => c.Name);
    }
}