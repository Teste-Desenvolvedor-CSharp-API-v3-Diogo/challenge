using Domain.Interfaces;
using Questao1.Application.Commands;
using Questao1.Domain.Entities;

namespace Questao1.Application.Handlers
{
    internal class CreateBankAccountHandler
    {
        private readonly IBankAccountRepository _repository;

        public CreateBankAccountHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public BankAccount Handle(CreateBankAccountCommand command)
        {
            var account = new BankAccount(command.AccountNumber, command.HolderName, command.InitialDeposit);
            _repository.Add(account);
            return account;
        }
    }
}
