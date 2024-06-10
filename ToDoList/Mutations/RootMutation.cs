using GraphQL.Types;

namespace ToDoList.Mutations;

public class RootMutation : ObjectGraphType
{
    public RootMutation()
    {
        Field<TaskMutation>("taskMutation").Resolve(context => new { });
    }
}