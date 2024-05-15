﻿namespace ToDoList.Domain.Entity;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int CategoryId { get; set; }
    public bool IsCompleted { get; set; }
}