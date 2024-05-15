namespace ToDoList.Domain.Entity;

public abstract class RepositoryBase
{
    private readonly string _connectionString;
    public RepositoryBase()
    {
        _connectionString = "Server=(localdb)\\mssqllocaldb; Database=TODOList; Integrated Security=true";
    }
    protected SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}