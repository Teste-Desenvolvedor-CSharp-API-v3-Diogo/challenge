using FluentValidation;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Questao5.Domain.Interfaces.Repositories;

namespace Questao5.Application.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IIdempotencyRepository idempotencyRepository;

        private readonly IValidator<CreateTransactionCommand> validator;

        public CreateTransactionCommandHandler(
            IBankAccountRepository bankAccountRepository,
            ITransactionRepository transactionRepository,
            IIdempotencyRepository idempotencyRepository,
            IValidator<CreateTransactionCommand> validator)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.transactionRepository = transactionRepository;
            this.idempotencyRepository = idempotencyRepository;
            this.validator = validator;
        }

        public async Task<string> Handle(CreateTransactionCommand command)
        {

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (await idempotencyRepository.ExistsAsync(command.IdempotencyKey))
            {
                return await idempotencyRepository.GetResult(command.IdempotencyKey);
            }

            var account = await bankAccountRepository.GetByNumberAsync(command.AccountNumber)
                           ?? throw new ArgumentException("Invalid account.");

            if (!account.IsActive)
                throw new InvalidOperationException("Account is inactive.");

            var transaction = account.CreateTransaction(
                (TransactionType) command.TransactionType,
                command.Amount
            );

            await transactionRepository.AddAsync(transaction);

            await idempotencyRepository.SaveResult(command.IdempotencyKey, transaction.Id.ToString());

            return transaction.Id.ToString();
        }
    }
}
