using Questao1.Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        void Add(BankAccount account);
        BankAccount GetByAccountNumber(int accountNumber);
        List<BankAccount> GetAll();
        void Update(BankAccount account);
    }
}
