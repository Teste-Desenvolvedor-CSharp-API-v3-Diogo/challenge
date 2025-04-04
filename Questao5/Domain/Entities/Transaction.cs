using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;

public class Transaction
{
    public string Id { get; private set; } = null!;
    public string BankAccountId { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }


    private Transaction() { }

    public Transaction(string id, string bankAccountId, decimal amount, TransactionType type, DateTime? date = null)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new DomainException(ErrorType.INVALID_TRANSACTION, "Transaction ID must be provided.");

        if (string.IsNullOrWhiteSpace(bankAccountId))
            throw new DomainException(ErrorType.INVALID_ACCOUNT, "Bank account ID must be provided.");

        if (amount <= 0)
            throw new DomainException(ErrorType.INVALID_VALUE, "Transaction amount must be greater than zero.");

        if (!Enum.IsDefined(typeof(TransactionType), type))
            throw new DomainException(ErrorType.INVALID_TYPE, "Transaction type must be C (credit) or D (debit).");

        Id = id;
        BankAccountId = bankAccountId;
        Amount = amount;
        Type = type;
        Date = date ?? DateTime.UtcNow;
    }
}
