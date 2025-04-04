using Questao5.Domain.Interfaces.Repositories;

namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQueryHandler
{
    private readonly IBankAccountRepository bankAccountRepository;
    private readonly ITransactionRepository transactionRepository;

    public GetAccountBalanceQueryHandler(
        IBankAccountRepository bankAccountRepository,
        ITransactionRepository transactionRepository)
    {
        this.bankAccountRepository = bankAccountRepository;
        this.transactionRepository = transactionRepository;
    }

    public async Task<GetAccountBalanceQueryResult> Handle(GetAccountBalanceQuery query)
    {
        var account = await bankAccountRepository.GetByNumberAsync(query.AccountNumber);

        if (account == null)
            throw new ArgumentException("Invalid account.");

        if (!account.IsActive)
            throw new InvalidOperationException("Account is inactive.");

        var balance = await transactionRepository.GetBalanceAsync(account.Id);

        return new GetAccountBalanceQueryResult(query.AccountNumber, balance);
    }
}
