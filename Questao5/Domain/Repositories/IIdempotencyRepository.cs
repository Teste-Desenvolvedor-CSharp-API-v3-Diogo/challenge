using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public interface IIdempotencyRepository
{
    Task<bool> ExistsAsync(string key);
    Task AddAsync(IdempotencyRecord record);
}
