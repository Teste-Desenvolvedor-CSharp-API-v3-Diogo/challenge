using FluentValidation;
using MediatR;
using Questao5.Application.Helpers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces.Repositories;

namespace Questao5.Application.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, string>
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

        public async Task<string> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            var requestHash = HashHelper.ComputeHash(command);

            if (await idempotencyRepository.ExistsAsync(command.IdempotencyKey))
            {
                var existingRequestHash = await idempotencyRepository.GetRequest(command.IdempotencyKey);
                if (existingRequestHash != requestHash)
                    throw new InvalidOperationException("A requisição com esta chave já foi feita com um conteúdo diferente.");

                return await idempotencyRepository.GetResult(command.IdempotencyKey);
            }

            var account = await bankAccountRepository.GetByNumberAsync(command.AccountNumber)
                           ?? throw new ArgumentException("Invalid account.");

            if (!account.IsActive)
                throw new InvalidOperationException("Account is inactive.");

            var transaction = account.CreateTransaction(
                (TransactionType) Enum.Parse(typeof(TransactionType), command.TransactionType),
                command.Amount
            );

            await transactionRepository.AddAsync(transaction);


            await idempotencyRepository.AddAsync(new IdempotencyUnit(
                Guid.NewGuid().ToString(),
                command.IdempotencyKey,
                requestHash,
                transaction.Id.ToString()
            ));

            return transaction.Id.ToString();
        }
    }
}
