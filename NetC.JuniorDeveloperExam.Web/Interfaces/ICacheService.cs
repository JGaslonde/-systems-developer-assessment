using NetC.JuniorDeveloperExam.Web.Models;
using System;

namespace NetC.JuniorDeveloperExam.Web.Interfaces
{
    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        void Update<T>(string cacheKey, Blog item) where T : class;
    }
}