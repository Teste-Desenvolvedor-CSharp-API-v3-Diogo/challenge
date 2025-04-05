using Microsoft.OpenApi.Models;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;

namespace Questao5.Domain.Entities;

public class BankAccount
{
    public string Id { get; }
    public string AccountNumber { get; }
    public string HolderName { get; }
    public bool IsActive { get; }

    public BankAccount() { }

    public BankAccount(string id, string accountNumber, string holderName, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new DomainException(ErrorType.INVALID_ACCOUNT, "Account ID must be provided.");

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new DomainException(ErrorType.INVALID_ACCOUNT, "Account number must be provided.");

        if (string.IsNullOrWhiteSpace(holderName))
            throw new DomainException(ErrorType.INVALID_ACCOUNT, "Holder name must be provided.");

        Id = id;
        AccountNumber = accountNumber;
        HolderName = holderName;
        IsActive = isActive;
    }

    public Transaction CreateTransaction(TransactionType transactionType
        , decimal amount)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot create transaction for an inactive account.");

        if (amount <= 0)
            throw new ArgumentException("Transaction amount must be greater than zero.");

        return new Transaction(
        Guid.NewGuid().ToString(),
        this.Id,
        amount,
        transactionType,
        DateTime.UtcNow
        );
    }

        public void EnsureIsActive()
    {
        if (!IsActive)
            throw new DomainException(ErrorType.INACTIVE_ACCOUNT, "Bank account is not active.");
    }
}
