using FluentValidation;

namespace Questao5.Application.Queries.GetAccountBalance;

public class GetAccountBalanceQueryValidator : AbstractValidator<GetAccountBalanceQuery>
{
    public GetAccountBalanceQueryValidator()
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("Account number must be provided.");
    }
}
