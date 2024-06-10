using System.Data;
using Microsoft.Data.SqlClient;

namespace ToDoList.Data;

public class ToDoContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public ToDoContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("ToDoConnectionString");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}