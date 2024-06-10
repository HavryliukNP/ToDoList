using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _sqlToDoListRepository;
        private readonly XmlTaskRepository _xmlToDoListRepository;
        private readonly CategoryRepository _sqlCategoryRepository;
        private readonly XmlCategoryRepository _xmlCategoryRepository;

        public TaskController(TaskRepository sqlToDoListRepository, XmlTaskRepository xmlToDoListRepository, 
                              CategoryRepository sqlCategoryRepository, XmlCategoryRepository xmlCategoryRepository)
        {
            _sqlToDoListRepository = sqlToDoListRepository;
            _xmlToDoListRepository = xmlToDoListRepository;
            _sqlCategoryRepository = sqlCategoryRepository;
            _xmlCategoryRepository = xmlCategoryRepository;
        }

        public IActionResult Index(bool useXml = false, bool isFromLink = false)
        {
            if (isFromLink)
            {
                TempData["useXml"] = useXml;
            }
            else if (TempData.ContainsKey("useXml"))
            {
                useXml = (bool)TempData["useXml"];
            }
            TempData.Keep("useXml");
            
            
            var tasks = useXml ? _xmlToDoListRepository.GetAllTasks() : _sqlToDoListRepository.GetAllTasks();
            var categories = useXml 
                ? _xmlCategoryRepository.GetAllCategories().ToDictionary(c => c.Id, c => c.Name)
                : _sqlCategoryRepository.GetAllCategories().ToDictionary(c => c.Id, c => c.Name);

            var viewModel = new TaskViewModel
            {
                Tasks = tasks,
                Categories = categories,
                UseXml = useXml
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            bool useXml = (bool)TempData["useXml"];

            if (useXml)
            {
                _xmlToDoListRepository.AddTask(task);
            }
            else
            {
                _sqlToDoListRepository.AddTask(task);
            }

            return RedirectToAction("Index", new { useXml });
        }
        
        public IActionResult UpdateTaskStatus(int taskId, bool isCompleted)
        {
            bool useXml = (bool)TempData["useXml"];

            if (useXml)
            {
                _xmlToDoListRepository.UpdateTaskStatus(taskId, isCompleted);
            }
            else
            {
                _sqlToDoListRepository.UpdateTaskStatus(taskId, isCompleted);
            }

            return RedirectToAction("Index", new { useXml });
        }
    }
}
