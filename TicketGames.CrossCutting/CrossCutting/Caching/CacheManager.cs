using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketGames.CrossCutting.CrossCutting.Settings;

namespace TicketGames.CrossCutting.CrossCutting.Caching
{
    public class CacheManager
    {
        public static ICacheProvider CacheProvider;

        private static bool CacheIsEnable
        {
            get
            {
                return SettingsManager.Provider.AppSettings.GetBool("Cache.Enabled");
            }
        }

        private static int CacheDefaultDatabase
        {
            get
            {
                return SettingsManager.Provider.AppSettings.GetInt32("Cache.Database");
            }
        }

        public static void SetProvider(ICacheProvider provider)
        {
            CacheProvider = provider;
        }

        public static object GetObject(Type type, string key)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return null;

                return CacheProvider.GetObject(type, GetKey(key));
            }
            else
            {
                return null;
            }
        }

        public static async Task<object> GetObjectAsync(Type type, string key)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return null;

                return await CacheProvider.GetObjectAsync(type, GetKey(key));
            }
            else
            {
                return null;
            }
        }

        public static T GetObject<T>(string key)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return default(T);

                return (T)CacheProvider.GetObject<T>(GetKey(key));
            }
            else
            {
                return default(T);
            }

        }

        public static List<T> GetHashsObjects<T>(string key, List<string> subKey)
        {
            return GetHashsObjects<T>(key, subKey, CacheDefaultDatabase);
        }

        public static List<T> GetHashsObjects<T>(string key, List<string> subKey, int database)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return null;

                return CacheProvider.GetHashsObjects<T>(GetKey(key), subKey, database);
            }
            else
            {
                return null;
            }

        }

        public static void SetHashsObjects(string key, Dictionary<string, string> hashValues)
        {
            SetHashsObjects(key, hashValues, CacheDefaultDatabase);
        }

        public static void SetHashsObjects(string key, Dictionary<string, string> hashValues, int database)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return;

                CacheProvider.SetHashsObjects(GetKey(key), hashValues, database);
            }
            else
            {
                return;
            }

        }


        public static async Task<T> GetObjectAsync<T>(string key)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return default(T);

                return await CacheProvider.GetObjectAsync<T>(GetKey(key));
            }
            else
            {
                return default(T);
            }

        }
        public static void StoreObject(string key, object value, LifetimeProfile profile)
        {
            if (CacheIsEnable)
                StoreObject(key, value, TimeSpan.FromMinutes((int)profile));
        }

        public static void StoreObject(string key, object value, TimeSpan expiry)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null || value == null)
                    return;

                CacheProvider.StoreObject(GetKey(key), value, expiry);
            }
        }

        public static string GetString(string key)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null)
                    return default(string);

                return CacheProvider.GetString(GetKey(key));
            }
            else
            {
                return default(string);
            }
        }

        public static async Task StoreObjectAsync(string key, object value, TimeSpan expiry)
        {
            if (CacheIsEnable)
            {
                if (CacheProvider == null || value == null)
                    return;

                await CacheProvider.StoreObjectAsync(GetKey(key), value, expiry);
            }
        }

        private static string GetKey(string key)
        {
            return key;
        }

        public static bool FlushEspecificDatabase()
        {
            return FlushEspecificDatabase(CacheDefaultDatabase);
        }

        public static bool FlushEspecificDatabase(int database)
        {
            if (CacheProvider == null)
                return false;

            CacheProvider.FlushEspecificDatabase(database);
            return true;
        }

        public static async Task<bool> KeyDeleteAsync(string key)
        {
            return await KeyDeleteAsync(key, CacheDefaultDatabase);
        }

        public static async Task<bool> KeyDeleteAsync(string key, int database)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            var cacheKeyDelete = await CacheProvider.KeyDeleteAsync(key, database);

            return cacheKeyDelete;
        }

        public static bool KeyDelete(string key)
        {
            return KeyDelete(key, CacheDefaultDatabase);
        }

        public static bool KeyDelete(string key, int database)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            var cacheKeyDelete = CacheProvider.KeyDelete(key, database);

            return cacheKeyDelete;

        }
    }
}
