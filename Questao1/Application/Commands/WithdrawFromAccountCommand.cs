using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1.Application.Commands
{
    public class WithdrawFromAccountCommand
    {
        public int AccountNumber { get; }
        public decimal Amount { get; }

        public WithdrawFromAccountCommand(int accountNumber, decimal amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }
    }
}
