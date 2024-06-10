using GraphQL.Types;
using ToDoList.Repositories;
using ToDoList.Type;

namespace ToDoList.Query;

public class TaskQuery : ObjectGraphType
{
    public TaskQuery(ITaskRepository taskRepository)
    {
        Field<ListGraphType<TaskType>>("tasks").Resolve(context =>
        {
            return taskRepository.GetAllTasks();
        });
    }
}