using System.Collections.Generic;
using System.Xml;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class XmlCategoryRepository : ICategoryRepository
    {
        private readonly XmlStorageContext _context;

        public XmlCategoryRepository(XmlStorageContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetAllCategories()
        {
            var categories = new List<CategoryModel>();
            var doc = new XmlDocument();
            doc.Load(_context.XmlFilePath);

            foreach (XmlNode node in doc.SelectNodes("DB/Categories/Category"))
            {
                categories.Add(new CategoryModel
                {
                    Id = int.Parse(node.SelectSingleNode("Id").InnerText),
                    Name = node.SelectSingleNode("Name").InnerText
                });
            }
            return categories;
        }
    }
}