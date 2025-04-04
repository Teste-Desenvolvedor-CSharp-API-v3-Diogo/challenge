using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1.Application.Commands
{
    internal class CreateBankAccountCommand
    {
        public int AccountNumber { get; }
        public string HolderName { get; }
        public decimal InitialDeposit { get; }

        public CreateBankAccountCommand(int accountNumber, string holderName, decimal initialDeposit)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            InitialDeposit = initialDeposit;
        }
    }
}
