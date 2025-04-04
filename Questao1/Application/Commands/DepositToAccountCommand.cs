using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1.Application.Commands
{
    internal class DepositToAccountCommand
    {
        public int AccountNumber { get; }
        public decimal Amount { get; }

        public DepositToAccountCommand(int accountNumber, decimal amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }
    }
}
