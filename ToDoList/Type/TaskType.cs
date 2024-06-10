using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Type
{
    public class TaskType : ObjectGraphType<TaskModel>
    {
        public TaskType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Description, nullable: true);
            Field(t => t.DueDate, nullable: true);
            Field(t => t.CategoryId);
            Field(t => t.IsCompleted);
        }
    }
}