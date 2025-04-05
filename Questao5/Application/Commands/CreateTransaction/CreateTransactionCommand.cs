using MediatR;
using Microsoft.OpenApi.Models;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<string>
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string TransactionType { get; set; } // "C" or "D"
        public decimal Amount { get; set; }
        public string IdempotencyKey { get; set; } = string.Empty;
    }
}
