using Dapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Persistence;

namespace Questao5.Infrastructure.Repositories.Implementations;

public class TransactionRepository : ITransactionRepository
{
    private readonly ConnectionFactory _connectionFactory;

    public TransactionRepository(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task AddAsync(Transaction transaction)
    {
        const string sql = @"
            INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
            VALUES (@Id, @BankAccountId, @Date, @Type, @Amount)";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, transaction);
    }

    public async Task<decimal> GetBalanceAsync(string bankAccountId)
    {
        const string sql = @"
            SELECT COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END), 0) 
            FROM movimento 
            WHERE idcontacorrente = @BankAccountId";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<decimal>(sql, new { BankAccountId = bankAccountId });
    }
}
