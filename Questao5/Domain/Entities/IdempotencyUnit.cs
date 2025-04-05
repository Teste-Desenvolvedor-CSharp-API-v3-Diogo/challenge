namespace Questao5.Domain.Entities;

public class IdempotencyUnit
{
    public string Id { get; }
    public string Key { get; }
    public string Request { get; }
    public string Result { get; }

    public IdempotencyUnit(string id, string key, string request, string result)
    {
        Id = id;
        Key = key;
        Request = request;
        Result = result;
    }
}
