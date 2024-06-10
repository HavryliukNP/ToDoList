using GraphQL.Types;

namespace ToDoList.Type;

public class CategoryInputType : InputObjectGraphType
{
    public CategoryInputType()
    {
        Field<IntGraphType>("id");
        Field<StringGraphType>("name");
    }
}