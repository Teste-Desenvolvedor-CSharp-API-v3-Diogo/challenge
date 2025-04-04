using Domain.Interfaces;
using Questao1.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class MockableBankAccountRepository : IBankAccountRepository
    {
        private readonly Dictionary<int, BankAccount> _accounts = new();

        public void Add(BankAccount account)
        {
            _accounts[account.AccountNumber] = account;
        }

        public BankAccount GetByAccountNumber(int accountNumber)
        {
            _accounts.TryGetValue(accountNumber, out var account);
            return account;
        }

        public List<BankAccount> GetAll()
        {
            return _accounts.Values.ToList();
        }

        public void Update(BankAccount account)
        {
            if (_accounts.ContainsKey(account.AccountNumber))
            {
                _accounts[account.AccountNumber] = account;
            }
        }
    }
}
