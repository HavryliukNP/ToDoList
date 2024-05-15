using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
    }

    public DbSet<TaskModel> Tasks { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
}