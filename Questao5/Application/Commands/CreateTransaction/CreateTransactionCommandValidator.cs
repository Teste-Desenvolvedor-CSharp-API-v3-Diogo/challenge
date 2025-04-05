using FluentValidation;
using Questao5.Application.Commands.CreateTransaction;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Validators;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.AccountNumber)
            .NotEmpty().WithMessage("Account number is required.");

        RuleFor(x => x.IdempotencyKey)
            .NotEmpty()
            .WithMessage("Idempotency key is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.TransactionType)
            .Must(type => type == TransactionType.C.ToString()
                       || type == TransactionType.D.ToString())
            .WithMessage("Operation type must be Credit or Debit.");
    }
}
