using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Repositories.Implementations;

public class IdempotencyRepository : IIdempotencyRepository
{
    private readonly ConnectionFactory _connectionFactory;

    public IdempotencyRepository(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> ExistsAsync(string key)
    {
        const string sql = "SELECT COUNT(1) FROM idempotencia WHERE chave_idempotencia = @Key";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<bool>(sql, new { Key = key });
    }

    public async Task AddAsync(IdempotencyRecord record)
    {
        const string sql = @"
            INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
            VALUES (@Id, @Request, @Result)";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, record);
    }
}
