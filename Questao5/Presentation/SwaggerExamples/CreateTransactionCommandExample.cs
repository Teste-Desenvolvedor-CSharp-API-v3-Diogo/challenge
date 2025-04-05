using Questao5.Application.Commands.CreateTransaction;
using Questao5.Domain.Enumerators;

namespace Questao5.Presentation.SwaggerExamples
{
    public class CreateTransactionCommandExample
    {
        public CreateTransactionCommand GetExamples()
        {
            return new CreateTransactionCommand
            {
                AccountNumber = "123456",
                IdempotencyKey = "abc-def-123",
                TransactionType = TransactionType.C.ToString(), // Crédito
                Amount = 100.50m
            };
        }
    }
}
