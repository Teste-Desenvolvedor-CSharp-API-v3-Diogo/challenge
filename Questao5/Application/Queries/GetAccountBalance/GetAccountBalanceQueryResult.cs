using System.Globalization;

namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQueryResult
{
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public DateTime DateTime { get; set; }
    public string Balance { get; set; }

    public GetAccountBalanceQueryResult(string accountNumber, string accountName, DateTime dateTime, decimal balance)
    {
        AccountNumber = accountNumber;
        AccountName = accountName;
        DateTime = dateTime;
        Balance = balance.ToString("F2", CultureInfo.InvariantCulture);
    }
}
