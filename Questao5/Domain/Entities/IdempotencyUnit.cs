namespace Questao5.Domain.Entities;

public class IdempotencyRecord
{
    public string Id { get; }
    public string Key { get; }
    public string Request { get; }
    public string Result { get; }

    public IdempotencyRecord(string id, string key, string request, string result)
    {
        Id = id;
        Key = key;
        Request = request;
        Result = result;
    }
}
