using GraphQL;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Repositories;
using ToDoList.Type;

namespace ToDoList.Mutations;

public class TaskMutation : ObjectGraphType
{
    public TaskMutation(ITaskRepository taskRepository)
    {
        Field<ListGraphType<TaskType>>("addTask").Arguments(new QueryArguments(new QueryArgument<TaskInputType>
        {
            Name = "task"
        })).Resolve(context =>
        {
            var task = context.GetArgument<TaskModel>("task");
            
            taskRepository.AddTask(task);
            
            return taskRepository.GetAllTasks();
        });
        
        Field<ListGraphType<TaskType>>("updateTaskStatus").Arguments(new QueryArguments(new QueryArgument<IntGraphType>
        {
            Name = "taskId"
        }, new QueryArgument<BooleanGraphType>
        {
            Name = "isCompleted"
        })).Resolve(context =>
        {
            var taskId = context.GetArgument<int>("taskId");
            
            var isCompleted = context.GetArgument<bool>("isCompleted");
            
            taskRepository.UpdateTaskStatus(taskId, isCompleted);
            
            return taskRepository.GetAllTasks();
        });
    }
}