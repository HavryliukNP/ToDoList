using GraphQL.Types;

namespace ToDoList.Query;

public class RootQuery : ObjectGraphType
{
    public RootQuery()
    {
        Field<TaskQuery>("taskQuery").Resolve(context => new { });
        Field<CategoryQuery>("categoryQuery").Resolve(context => new { });
    }
}