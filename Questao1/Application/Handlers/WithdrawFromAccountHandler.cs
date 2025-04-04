using Domain.Interfaces;
using Questao1.Application.Commands;
using Questao1.Domain.Entities;

namespace Questao1.Application.Handlers
{
    internal class WithdrawFromAccountHandler
    {
        private readonly IBankAccountRepository _repository;

        public WithdrawFromAccountHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public BankAccount Handle(WithdrawFromAccountCommand command)
        {
            var account = _repository.GetByAccountNumber(command.AccountNumber);
            account.Withdraw(command.Amount);
            _repository.Update(account);
            return account;
        }
    }
}
