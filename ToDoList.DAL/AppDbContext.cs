using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entity;

namespace ToDoList.DAL;

public class AppDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }
}