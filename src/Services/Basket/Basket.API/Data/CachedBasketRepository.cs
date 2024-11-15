
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    /// <summary>
    /// Decorate the IBasketRepository.
    /// The decorator pattern is a structural design pattern that allows behavior to be added to an existing object dynamically. It's like adding responsibilities to an object without affecting other objects from the same class.
    /// Decorator is focused on adding responsibilities to an object dynamically.
    /// Proxy is more about controlling access or adding a level of indirection to an object.
    /// CachedBasketRepository can act as a proxy, since it controlls the access whether to request to the DB or to the Cache.
    /// </summary>
    /// <param name="repository"></param>
    public class CachedBasketRepository(
        IBasketRepository repository,
        IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

            var basket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            var addedBasket = await repository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(addedBasket.UserName, JsonSerializer.Serialize(addedBasket), cancellationToken);

            return addedBasket;
        }
    }
}
