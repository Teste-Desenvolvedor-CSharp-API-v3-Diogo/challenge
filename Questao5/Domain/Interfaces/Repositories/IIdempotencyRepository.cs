using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces.Repositories;

public interface IIdempotencyRepository
{
    Task<bool> ExistsAsync(string key);
    Task AddAsync(IdempotencyRecord record); 
    Task<string> GetResult(string key);
    Task SaveResult(string key, string result);
}
