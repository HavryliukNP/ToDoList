namespace ToDoList.Data;

public class XmlStorageContext
{
    private readonly IConfiguration configuration;
    private readonly string? xmlFilePath;

    public XmlStorageContext(IConfiguration configuration)
    {
        this.configuration = configuration;
        xmlFilePath = this.configuration.GetConnectionString("XmlFilePath");
    }

    public string XmlFilePath => xmlFilePath;
}