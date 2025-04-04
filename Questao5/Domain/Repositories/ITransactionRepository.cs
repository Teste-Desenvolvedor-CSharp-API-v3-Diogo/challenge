using Questao5.Domain.Entities;
using Questao5.Domain.Enums;

namespace Questao5.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<decimal> GetBalanceAsync(string bankAccountId);
}
