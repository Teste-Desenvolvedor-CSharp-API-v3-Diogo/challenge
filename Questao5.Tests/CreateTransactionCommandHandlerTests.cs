using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Questao5.Application.Commands.CreateTransaction;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces.Repositories;
using Xunit;

public class CreateTransactionCommandHandlerTests
{
    private readonly Mock<IBankAccountRepository> bankAccountRepoMock = new();
    private readonly Mock<ITransactionRepository> transactionRepoMock = new();
    private readonly Mock<IIdempotencyRepository> idempotencyRepoMock = new();
    private readonly Mock<IValidator<CreateTransactionCommand>> validatorMock = new();

    private CreateTransactionCommandHandler handler;

    public CreateTransactionCommandHandlerTests()
    {
        handler = new CreateTransactionCommandHandler(
            bankAccountRepoMock.Object,
            transactionRepoMock.Object,
            idempotencyRepoMock.Object,
            validatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CreateTransactionCommand();
        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Amount", "Required") }));

        // Act
        var act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenAccountNotFound()
    {
        var command = new CreateTransactionCommand
        {
            AccountNumber = "123",
            Amount = 100,
            TransactionType = "CREDIT",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        validatorMock.Setup(v => v.ValidateAsync(command, default))
            .ReturnsAsync(new ValidationResult());
        idempotencyRepoMock.Setup(r => r.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        bankAccountRepoMock.Setup(r => r.GetByNumberAsync("123")).ReturnsAsync((BankAccount)null);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid account.");
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenAccountIsInactive()
    {
        var command = new CreateTransactionCommand
        {
            AccountNumber = "123",
            Amount = 100,
            TransactionType = "DEBIT",
            IdempotencyKey = Guid.NewGuid().ToString()
        };

        validatorMock.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new ValidationResult());
        idempotencyRepoMock.Setup(r => r.ExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        bankAccountRepoMock.Setup(r => r.GetByNumberAsync("123"))
            .ReturnsAsync(new BankAccount("B6BAFC09-6967-ED11-A567-055DFA4A16C9", "123", "João", false));

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Account is inactive.");
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenIdempotencyKeyReusedWithDifferentContent()
    {
        var command = new CreateTransactionCommand
        {
            AccountNumber = "123",
            Amount = 100,
            TransactionType = "CREDIT",
            IdempotencyKey = "idem-key"
        };

        validatorMock.Setup(v => v.ValidateAsync(command, default)).ReturnsAsync(new ValidationResult());
        idempotencyRepoMock.Setup(r => r.ExistsAsync("idem-key")).ReturnsAsync(true);
        idempotencyRepoMock.Setup(r => r.GetRequest("idem-key")).ReturnsAsync("diferente-hash");

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("A requisição com esta chave já foi feita com um conteúdo diferente.");
    }

}
