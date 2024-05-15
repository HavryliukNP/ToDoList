using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Controllers;
public class TaskController : Controller
{
    private readonly CategoryRepository _categoryRepository;
    private readonly ToDoContext _context;

    private readonly TaskRepository _taskRepository;

    public TaskController(ToDoContext context)
    {
        _context = context;
        _taskRepository = new TaskRepository(context);
        _categoryRepository = new CategoryRepository(context);
    }

    public IActionResult Index()
    {
        var tasks = _taskRepository.GetAllTask();
        var categories = _categoryRepository.GetAllCategories().ToDictionary(c => c.Id, c => c.Name);

        var viewModel = new CategoryViewModel
        {
            Tasks = tasks,
            Categories = categories
        };

        return View(viewModel);
    }


    [HttpPost]
    public IActionResult Create(TaskModel task)
    {
        _taskRepository.AddTask(task);
        return RedirectToAction("Index");
    }
    
    public IActionResult List()
    {
        var tasks = _taskRepository.GetAllTask();
        var categories = _categoryRepository.GetAllCategories().ToDictionary(c => c.Id, c => c.Name);

        var viewModel = new CategoryViewModel
        {
            Tasks = tasks,
            Categories = categories
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult UpdateTaskStatus(int taskId, bool isCompleted)
    {
        _taskRepository.UpdateTaskStatus(taskId, isCompleted);
        return RedirectToAction(nameof(Index));
    }
}