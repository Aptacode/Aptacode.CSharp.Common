using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Aptacode.CSharp.Common.Persistence.Cache
{
    public class GenericMemoryCache<TKey, TValue>
    {
        private readonly MemoryCache _cache;
        public TimeSpan RelativeExpiration = new TimeSpan(0, 10, 0);

        public GenericMemoryCache() : this(new MemoryCacheOptions()) { }

        public GenericMemoryCache(MemoryCacheOptions memoryCacheOptions)
        {
            _cache = new MemoryCache(memoryCacheOptions);
        }

        public async Task<TValue> GetOrCreate(TKey key, Func<Task<TValue>> createItem)
        {
            if (_cache.TryGetValue(key, out TValue cacheEntry))
            {
                return cacheEntry;
            }

            // Key not in cache, so get data.
            cacheEntry = await createItem().ConfigureAwait(false);

            // Save data in cache.
            _cache.Set(key, cacheEntry, RelativeExpiration);
            return cacheEntry;
        }

        public void Update(TKey key, TValue entity)
        {
            _cache.Set(key, entity);
        }

        public void Remove(TKey key)
        {
            _cache.Remove(key);
        }
    }
}