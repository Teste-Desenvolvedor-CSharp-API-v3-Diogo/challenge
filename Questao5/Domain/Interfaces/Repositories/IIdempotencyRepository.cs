using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Repositories;

public interface IIdempotencyRepository
{
    Task<bool> ExistsAsync(string key);
    Task AddAsync(IdempotencyUnit record);
    Task<string> GetRequest(string key);

    Task<string> GetResult(string key);
    Task SaveResult(string key, string result);
}
