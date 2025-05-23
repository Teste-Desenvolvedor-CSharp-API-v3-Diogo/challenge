﻿using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces.Repositories;
using Questao5.Infrastructure.Persistence;

namespace Questao5.Infrastructure.Repositories.Implementations;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ConnectionFactory _connectionFactory;

    public BankAccountRepository(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<BankAccount?> GetByIdAsync(string id)
    {
        const string sql = "SELECT idcontacorrente AS Id, numero AS AccountNumber, nome AS HolderName, ativo AS IsActive FROM contacorrente WHERE idcontacorrente = @Id";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<BankAccount>(sql, new { Id = id });
    }

    public async Task<BankAccount?> GetByNumberAsync(string accountNumber)
    {
        const string sql = @"SELECT idcontacorrente AS Id, numero AS AccountNumber, nome AS HolderName, ativo AS IsActive FROM contacorrente WHERE numero = @AccountNumber";

        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<BankAccount>(sql, new { AccountNumber = accountNumber });
    }
}
