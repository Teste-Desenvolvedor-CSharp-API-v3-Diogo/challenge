using MediatR;

namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQuery : IRequest<GetAccountBalanceQueryResult>
{
    public string AccountNumber { get; set; }

    public GetAccountBalanceQuery(string accountNumber)
    {
        AccountNumber = accountNumber;
    }
}
