using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _redis;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        var data = await _redis.StringGetAsync(basketId);
        var basket = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        return basket;
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var data = JsonSerializer.Serialize(basket);
        var created = await _redis.StringSetAsync(basket.Id, data, TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetBasketAsync(basket.Id);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        var result = await _redis.KeyDeleteAsync(basketId);
        return result;
    }
}
