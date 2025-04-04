using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public interface IAccountRepository
{
    Task<BankAccount?> GetByIdAsync(string id);
    Task<BankAccount?> GetByNumberAsync(string accountNumber);
}
