using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories
{
    public class IdempotencyRepository : IIdempotencyRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotencyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            const string sql = "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @Key";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Key = key });
            return count > 0;
        }

        public async Task AddAsync(IdempotencyRecord record)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            const string sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@Key, @Request, @Result)";
            await connection.ExecuteAsync(sql, new
            {
                Key = record.Key,
                Request = record.Request,
                Result = record.Result
            });
        }

        public async Task<string> GetResult(string key)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            const string sql = "SELECT resultado FROM idempotencia WHERE chave_idempotencia = @Key";
            return await connection.QuerySingleOrDefaultAsync<string>(sql, new { Key = key }) ?? string.Empty;
        }

        public async Task SaveResult(string key, string result)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            const string sql = "UPDATE idempotencia SET resultado = @Result WHERE chave_idempotencia = @Key";
            await connection.ExecuteAsync(sql, new { Key = key, Result = result });
        }
    }
}
