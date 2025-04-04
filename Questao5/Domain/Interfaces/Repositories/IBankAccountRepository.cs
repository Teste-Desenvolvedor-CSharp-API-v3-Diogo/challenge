using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Repositories;

public interface IBankAccountRepository
{
    Task<BankAccount?> GetByIdAsync(string id);
    Task<BankAccount?> GetByNumberAsync(string accountNumber);
}
