using Domain.Interfaces;
using Questao1.Application.Commands;
using Questao1.Domain.Entities;
using System.Collections.Generic;

namespace Questao1.Application.Handlers
{
    public class ListAccountsHandler
    {
        private readonly IBankAccountRepository _repository;

        public ListAccountsHandler(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public List<BankAccount> Handle(ListAccountsCommand command)
        {
            return _repository.GetAll();
        }
    }
}
