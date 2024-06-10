using GraphQL.Types;

namespace ToDoList.Type;

public class TaskInputType : InputObjectGraphType
{
    public TaskInputType()
    {
        Field<IntGraphType>("id");
        Field<StringGraphType>("title");
        Field<StringGraphType>("description");
        Field<DateTimeGraphType>("dueDate");
        Field<IntGraphType>("categoryId");
        Field<BooleanGraphType>("isCompleted");
    }
}