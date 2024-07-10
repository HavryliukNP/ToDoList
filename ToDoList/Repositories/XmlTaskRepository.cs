using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories
{
   public class XmlTaskRepository : ITaskRepository
    {
         private readonly XmlStorageContext _context;

        public XmlTaskRepository(XmlStorageContext context)
        {
            _context = context;
        }

        public List<TaskModel> GetAllTasks()
        {
            var tasks = new List<TaskModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            foreach (XmlNode node in doc.SelectNodes("DB/Tasks/Task"))
            {
                tasks.Add(new TaskModel
                {
                    Id = int.Parse(node.SelectSingleNode("Id").InnerText),
                    Title = node.SelectSingleNode("Title").InnerText,
                    Description = node.SelectSingleNode("Description").InnerText,
                    DueDate = DateTime.TryParse(node.SelectSingleNode("DueDate").InnerText, out var dueDate) ? (DateTime?)dueDate : null,
                    CategoryId = int.Parse(node.SelectSingleNode("CategoryId").InnerText),
                    IsCompleted = bool.Parse(node.SelectSingleNode("IsCompleted").InnerText)
                });
            }
    
            tasks = tasks.OrderBy(t => t.IsCompleted).ThenByDescending(t => t.Id).ToList();

            return tasks;
        }
        
        public void AddTask(TaskModel task)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            XmlElement newTask = doc.CreateElement("Task");

            XmlElement id = doc.CreateElement("Id");
            id.InnerText = GetUniqueIntId().ToString();
            newTask.AppendChild(id);

            XmlElement title = doc.CreateElement("Title");
            title.InnerText = task.Title;
            newTask.AppendChild(title);

            XmlElement description = doc.CreateElement("Description");
            description.InnerText = task.Description;
            newTask.AppendChild(description);

            XmlElement dueDate = doc.CreateElement("DueDate");
            dueDate.InnerText = task.DueDate?.ToString("yyyy-MM-dd");
            newTask.AppendChild(dueDate);

            XmlElement categoryId = doc.CreateElement("CategoryId");
            categoryId.InnerText = task.CategoryId.ToString();
            newTask.AppendChild(categoryId);

            XmlElement isCompleted = doc.CreateElement("IsCompleted");
            isCompleted.InnerText = task.IsCompleted.ToString();
            newTask.AppendChild(isCompleted);

            XmlNode tasksNode = doc.SelectSingleNode("DB/Tasks");
            tasksNode.AppendChild(newTask);
            doc.Save(_context.XmlFilePath);
        }

        private int GetUniqueIntId()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            XmlNodeList nodes = doc.SelectNodes("DB/Tasks/Task/Id");
            int maxId = 0;

            foreach (XmlNode node in nodes)
            {
                int currentId = int.Parse(node.InnerText);
                if (currentId > maxId)
                {
                    maxId = currentId;
                }
            }

            return maxId + 1;
        }
        
        public void UpdateTaskStatus(int taskId, bool isCompleted)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            XmlNode taskToUpdate = doc.SelectSingleNode($"DB/Tasks/Task[Id='{taskId}']");
            if (taskToUpdate != null)
            {
                taskToUpdate.SelectSingleNode("IsCompleted").InnerText = isCompleted.ToString();
                doc.Save(_context.XmlFilePath);
            }
        }
        public void DeleteTask(int taskId)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            XmlNode taskToDelete = doc.SelectSingleNode($"DB/Tasks/Task[Id='{taskId}']");
            if (taskToDelete != null)
            {
                XmlNode tasksNode = doc.SelectSingleNode("DB/Tasks");
                tasksNode.RemoveChild(taskToDelete);
                doc.Save(_context.XmlFilePath);
            }
        }

    }
}
