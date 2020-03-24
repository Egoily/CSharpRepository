using System;
using System.Runtime.Caching;

namespace ee.Core.Framework.Caching
{
    /// <summary>
    /// MemoryCache
    /// </summary>
    public static class MemoryCache
    {
        private static readonly object _lockobj = new object();

        public static T CacheItem<T>(string key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null, bool immediatelyUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Invalid cache key");
            }

            if (cachePopulate == null)
            {
                throw new ArgumentNullException("cachePopulate");
            }

            if (slidingExpiration == null && absoluteExpiration == null)
            {
                throw new ArgumentException("Either a sliding expiration or absolute must be provided");
            }

            if (immediatelyUpdate && System.Runtime.Caching.MemoryCache.Default[key] != null)
            {
                System.Runtime.Caching.MemoryCache.Default.Remove(key);
            }
            if (System.Runtime.Caching.MemoryCache.Default[key] == null)
            {
                lock (_lockobj)
                {
                    if (System.Runtime.Caching.MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        System.Runtime.Caching.MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)System.Runtime.Caching.MemoryCache.Default[key];
        }

        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
    }
}
