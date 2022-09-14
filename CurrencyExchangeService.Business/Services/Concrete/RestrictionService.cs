using CurrencyExchangeService.Business.Services.Abstract;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace CurrencyExchangeService.Business.Services.Concrete
{
    public class RestrictionService : IRestrictionService
    {
        private readonly IDistributedCache _cache;
        
        public RestrictionService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public bool IsTradable(string accountNumber, int tradableCount)
        {
            var key = $"accountNumber:{accountNumber}";

            var response = _cache.GetString(key);

            var tradeCount = string.IsNullOrWhiteSpace(response) ? 0 : Convert.ToInt32(response);

            if(tradableCount > tradeCount)
            {
                var cacheEntryOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(1)
                };

                _cache.SetString(key, (++tradeCount).ToString(), cacheEntryOptions);

                return true;
            }

            return false;
        }
    }
}
