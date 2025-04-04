using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Infrastructure.Persistence;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Repositories.Implementations;


public class TransactionRepository : ITransactionRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public TransactionRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task AddAsync(Transaction transaction)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        const string sql = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @BankAccountId, @Date, @Type, @Amount)";
        await connection.ExecuteAsync(sql, new
        {
            Id = transaction.Id,
            BankAccountId = transaction.BankAccountId,
            Date = transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"),
            Type = transaction.Type.ToString(),
            Amount = transaction.Amount
        });
    }

    public async Task<decimal> GetBalanceAsync(string bankAccountId)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);
        const string sql = @"
                SELECT 
                    COALESCE(SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE -valor END), 0) 
                FROM movimento 
                WHERE idcontacorrente = @BankAccountId";

        return await connection.ExecuteScalarAsync<decimal>(sql, new { BankAccountId = bankAccountId });
    }
}
