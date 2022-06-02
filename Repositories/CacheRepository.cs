using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using SocialApp.Database;
using SocialAppService.Infrastructure;

namespace SocialAppService.Repositories
{
    public class CacheRepository<T> where T : class, EntityBase
    {        
        private const int CacheTimeToLive = 120;
        private readonly ICacheProvider cacheProvider;
        private static readonly SemaphoreSlim Semaphore = new(1, 1);
        private readonly DistributedCacheEntryOptions cacheEntryOptions;

        public CacheRepository(ICacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
            this.cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(CacheTimeToLive));
        }

        public async Task SetCache(string key, T value)
        {
             await cacheProvider.SetCache(key, value, cacheEntryOptions);
        }

        public async Task<T> GetFromCache(int key, Func<Task<T>> func)
        {
             return await GetCachedResponse (key, Semaphore, func);
        }

        public async Task ClearCache(string key)
        {
            await cacheProvider.ClearCache(key);
        }


        private async Task<T> GetCachedResponse(int cacheKey, SemaphoreSlim semaphore, Func<Task<T>> func)
        {
            var key = Convert.ToString(cacheKey);
            var obj = await cacheProvider.GetFromCache<T>(key);

            if (obj != null) return obj;
            try 
            {
                await semaphore.WaitAsync();
                obj = await cacheProvider.GetFromCache<T>(key);
                if (obj != null) return obj;
                
                obj = await func(); 
                await cacheProvider.SetCache(key, obj, cacheEntryOptions);
            }
            finally
            {
                semaphore.Release();
            }
            return obj;
        }
    }
}