using System;

namespace Questao1.Domain.Entities
{
    public class BankAccount
    {
        public int AccountNumber { get; }
        public string HolderName { get; private set; }
        public decimal Balance { get; private set; }

        private const decimal WithdrawalFee = 3.50m;

        public BankAccount(int accountNumber, string holderName, decimal initialDeposit = 0)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            Balance = initialDeposit;
        }

        public void UpdateHolderName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Holder name cannot be empty.");

            HolderName = newName;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            Balance -= amount + WithdrawalFee;
        }

        public override string ToString()
        {
            return $"Account {AccountNumber}, Holder: {HolderName}, Balance: ${Balance:F2}";
        }
    }
}
