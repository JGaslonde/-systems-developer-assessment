using NetC.JuniorDeveloperExam.Web.Interfaces;
using NetC.JuniorDeveloperExam.Web.Models;
using System;
using System.Runtime.Caching;

namespace NetC.JuniorDeveloperExam.Web.Services
{
    public class InMemoryCacheService : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                if (item == null)
                    return null;

                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }

        public void Update<T>(string cacheKey, Blog item) where T : class
        {
            if (item != null)
            {
                MemoryCache.Default.Remove(cacheKey);
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
        }
    }
}