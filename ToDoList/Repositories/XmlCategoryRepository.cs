using System.Xml;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class XmlCategoryRepository
    {
        private readonly string _xmlFilePath;

        public XmlCategoryRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public List<CategoryModel> GetAllCategories()
        {
            var categories = new List<CategoryModel>();
            var doc = new XmlDocument();
            doc.Load(_xmlFilePath);
            
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