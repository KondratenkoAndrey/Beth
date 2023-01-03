using System.Text.Json;
using Beth.Identity.Domain.Authenticate;
using Beth.Identity.Domain.Interfaces;
using StackExchange.Redis;

namespace Beth.Identity.Infrastructure.Repositories;

public class OneTimeCodeRepository : IOneTimeCodeRepository
{
    private readonly IDatabase _database;
    
    private static JsonSerializerOptions JsonSerializerOptions => new() { PropertyNameCaseInsensitive = true };

    public OneTimeCodeRepository(ConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task AddCodeAsync(OneTimeCode code)
    {
        var created = await _database.StringSetAsync(
            code.MobilePhone,
            JsonSerializer.Serialize(code),
            code.Duration);

        if (!created)
        {
            throw new Exception($"Одноразовый код для абонента {code.MobilePhone} не записан в БД");
        }
    }
    
    public async Task<OneTimeCode> FindCodeAsync(string mobilePhone)
    {
        var data = await _database.StringGetAsync(mobilePhone);
        if (data.IsNullOrEmpty)
        {
            return null;
        }
        
        return JsonSerializer.Deserialize<OneTimeCode>(data, JsonSerializerOptions);
    }
}