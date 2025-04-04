using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Exceptions;

public class DomainException : Exception
{
    public ErrorType Type { get; }

    public DomainException(ErrorType type, string message) : base(message)
    {
        Type = type;
    }
}
