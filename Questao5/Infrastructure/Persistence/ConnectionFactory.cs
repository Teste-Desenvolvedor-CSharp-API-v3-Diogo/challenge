using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Persistence;

public class ConnectionFactory
{
    private readonly string _connectionString;

    public ConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SQLite")
                            ?? throw new ArgumentNullException("Database connection string is missing.");
    }

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}
