using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.CrossCutting.CrossCutting.Caching
{
    public static class CacheInvoke
    {
        public static ReturnType InvokeWithCache<ReturnType>(LifetimeProfile profile, string key, Func<ReturnType> action)
        {
            object obj = CacheManager.GetObject(typeof(ReturnType), key);
            if (obj == null)
            {
                obj = action.Invoke();
                CacheManager.StoreObject(key, obj, TimeSpan.FromMinutes((int)profile));
            }
            return (ReturnType)obj;
        }

        public static async Task<ReturnType> InvokeWithCacheAsync<ReturnType>(LifetimeProfile profile, string key, Func<Task<ReturnType>> action)
        {
            object obj = await CacheManager.GetObjectAsync(typeof(ReturnType), key);
            if (obj == null)
            {
                obj = await action.Invoke();
                if (obj != null)
                    CacheManager.StoreObjectAsync(key, obj, TimeSpan.FromMinutes((int)profile));
            }
            return (ReturnType)obj;
        }
    }
}
