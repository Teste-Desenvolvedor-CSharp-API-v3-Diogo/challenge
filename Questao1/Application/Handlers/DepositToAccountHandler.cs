using Domain.Interfaces;
using Questao1.Application.Commands;
using Questao1.Domain.Entities;

namespace Questao1.Application.Handlers
{
    internal class DepositToAccountHandler
    {
        private readonly IBankAccountRepository _repository;

        public DepositToAccountHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public BankAccount Handle(DepositToAccountCommand command)
        {
            var account = _repository.GetByAccountNumber(command.AccountNumber);
            account.Deposit(command.Amount);
            _repository.Update(account);
            return account;
        }
    }
}
