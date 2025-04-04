namespace Questao5.Domain.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<decimal> GetBalanceAsync(string bankAccountId);
}
