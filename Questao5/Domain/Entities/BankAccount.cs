using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;

namespace Questao5.Domain.Entities;

public class BankAccount
{
    public string Id { get; }
    public string AccountNumber { get; }
    public string HolderName { get; }
    public bool IsActive { get; }

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

    public void EnsureIsActive()
    {
        if (!IsActive)
            throw new DomainException(ErrorType.INACTIVE_ACCOUNT, "Bank account is not active.");
    }
}
