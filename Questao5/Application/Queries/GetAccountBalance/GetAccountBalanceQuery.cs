namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQuery
{
    public string AccountNumber { get; set; }

    public GetAccountBalanceQuery(string accountNumber)
    {
        AccountNumber = accountNumber;
    }
}
