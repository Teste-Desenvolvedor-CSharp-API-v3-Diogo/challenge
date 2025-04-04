namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQueryResult
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public GetAccountBalanceQueryResult(string accountNumber, decimal balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
    }
}
