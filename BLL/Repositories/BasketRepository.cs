using BLL.Repositories.Interfaces;
using DLL.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<Basket> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            
            if (string.IsNullOrEmpty(basket)) return null;

            return JsonConvert.DeserializeObject<Basket>(basket);
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName); 
        }
    }
}
