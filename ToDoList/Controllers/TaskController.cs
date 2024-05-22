using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepository _sqlTaskRepository;
        private readonly XmlTaskRepository _xmlTaskRepository;
        private readonly CategoryRepository _sqlCategoryRepository;
        private readonly XmlCategoryRepository _xmlCategoryRepository;

        public TaskController(TaskRepository sqlTaskRepository, XmlTaskRepository xmlTaskRepository, 
                              CategoryRepository sqlCategoryRepository, XmlCategoryRepository xmlCategoryRepository)
        {
            _sqlTaskRepository = sqlTaskRepository;
            _xmlTaskRepository = xmlTaskRepository;
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
            
            
            var tasks = useXml ? _xmlTaskRepository.GetAllTasks() : _sqlTaskRepository.GetAllTasks();
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
                _xmlTaskRepository.AddTask(task);
            }
            else
            {
                _sqlTaskRepository.AddTask(task);
            }

            return RedirectToAction("Index", new { useXml });
        }
        
        public IActionResult UpdateTaskStatus(int taskId, bool isCompleted)
        {
            bool useXml = (bool)TempData["useXml"];

            if (useXml)
            {
                _xmlTaskRepository.UpdateTaskStatus(taskId, isCompleted);
            }
            else
            {
                _sqlTaskRepository.UpdateTaskStatus(taskId, isCompleted);
            }

            return RedirectToAction("Index", new { useXml });
        }
    }
}
