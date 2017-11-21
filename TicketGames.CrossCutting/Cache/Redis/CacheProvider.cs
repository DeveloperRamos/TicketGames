using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.CrossCutting.Cache.Redis
{
    public class CacheProvider : ICacheProvider
    {
        private const string REDIS_PASSWORD = "Cache.Redis.Password";
        private const string REDIS_SERVER = "Cache.Redis.Server";
        private const int TIMEOUT = 5000;

        static ConnectionMultiplexer _connectionMultiplexer = OpenRedis();
        static JsonSerializerSettings _jsonSettings = GetJsonSettings();
        static int _cacheDefaultDatabase = Settings.SettingsManager.Provider.AppSettings.GetInt32("Cache.Database");

        private static JsonSerializerSettings GetJsonSettings()
        {
            var resolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() { DefaultMembersSearchFlags = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance };

            var settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = resolver
            };

            return settings;
        }
        private static ConnectionMultiplexer OpenRedis()
        {
            var settings = CrossCutting.Settings.SettingsManager.Provider.AppSettings;

            var config = new ConfigurationOptions
            {
                Password = settings[REDIS_PASSWORD],
                AbortOnConnectFail = false,
                AllowAdmin = true,
                ConnectTimeout = TIMEOUT,
                ResponseTimeout = TIMEOUT
            };

            config.EndPoints.Add(settings[REDIS_SERVER]);

            return ConnectionMultiplexer.Connect(config);
        }
        public object GetObject(Type type, string key)
        {
            try
            {
                string json = GetRedis(key);

                return ConvertObject(type, json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<object> GetObjectAsync(Type type, string key)
        {
            try
            {
                string json = await GetRedisAsync(key);

                return ConvertObject(type, json);
            }
            catch
            {
                return null;
            }
        }

        private object ConvertObject(Type type, string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonConvert.DeserializeObject(json, type, _jsonSettings);
        }

        private string GetRedis(string key)
        {
            return GetRedis(key, _cacheDefaultDatabase);
        }

        private string GetRedis(string key, int database)
        {
            var db = GetDatabase(database);

            return db.StringGet(key);
        }

        private string GetHashRedis(string key, string subKey)
        {
            return GetHashRedis(key, subKey, _cacheDefaultDatabase);
        }

        private string GetHashRedis(string key, string subKey, int database)
        {
            var db = GetDatabase(database);

            return db.HashGet(key, subKey);
        }

        private RedisValue[] GetHashsRedis(string key, List<RedisValue> subKeys)
        {
            return GetHashsRedis(key, subKeys, _cacheDefaultDatabase);
        }

        private RedisValue[] GetHashsRedis(string key, List<RedisValue> subKeys, int database)
        {
            var db = GetDatabase(database);

            var hashs = db.HashGet(key, subKeys.ToArray());

            return hashs;
        }

        private async Task<string> GetRedisAsync(string key)
        {
            var db = GetDatabase();

            return await db.StringGetAsync(key);
        }

        public T GetObject<T>(string key)
        {
            return GetObject<T>(key, _cacheDefaultDatabase);
        }

        public T GetObject<T>(string key, int database)
        {
            try
            {
                var db = GetDatabase(database);
                string json = GetRedis(key, database);

                return ConvertObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        public T GetHashObject<T>(string key, string subKey)
        {
            return GetHashObject<T>(key, subKey, _cacheDefaultDatabase);
        }

        public T GetHashObject<T>(string key, string subKey, int database)
        {
            try
            {
                string json = GetHashRedis(key, subKey, database);

                return ConvertObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        public void SetHashsObjects(string key, Dictionary<string, string> hashValues)
        {
            SetHashsObjects(key, hashValues, _cacheDefaultDatabase);
        }

        public void SetHashsObjects(string key, Dictionary<string, string> hashValues, int database)
        {
            var db = GetDatabase(database);

            var keys = hashValues.Select(x => new HashEntry(x.Key, x.Value));
            db.HashSetAsync(key, keys.ToArray());
        }

        public List<T> GetHashsObjects<T>(string key, List<string> subKey)
        {
            return GetHashsObjects<T>(key, subKey, _cacheDefaultDatabase);
        }

        public List<T> GetHashsObjects<T>(string key, List<string> subKey, int database)
        {
            try
            {
                List<RedisValue> redisValues = subKey.ConvertAll<RedisValue>(x => x);

                var hashs = GetHashsRedis(key, redisValues, database);
                List<T> list = new List<T>();
                foreach (var item in hashs)
                {
                    if (!item.IsNullOrEmpty)
                        list.Add(ConvertObject<T>(item));
                }

                //List<string> hashReturn = subKey.ConvertAll<string>(x => x);

                //List<T> list = new List<T>();

                //foreach (var item in hashReturn)
                //{
                //    if(!string.IsNullOrEmpty(item))
                //        list.Add(ConvertObject<T>(item));
                //}

                return list;

            }
            catch
            {
                return null;
            }
        }

        public string GetHashObject(string key, string subKey)
        {
            return GetHashObject(key, subKey, _cacheDefaultDatabase);
        }

        public string GetHashObject(string key, string subKey, int database)
        {
            try
            {
                return GetHashRedis(key, subKey, database);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<T> GetObjectAsync<T>(string key)
        {
            try
            {
                string json = await GetRedisAsync(key);

                return ConvertObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        private T ConvertObject<T>(string result)
        {
            if (string.IsNullOrEmpty(result))
                return default(T);

            return JsonConvert.DeserializeObject<T>(result, _jsonSettings);
        }

        public async Task StoreObjectAsync(string key, object value, TimeSpan expiry)
        {
            try
            {
                if (value == null)
                    return;

                var db = GetDatabase();
                var obj = JsonConvert.SerializeObject(value);
                await db.StringSetAsync(key, obj, expiry);
            }
            catch
            {
            }
        }

        public void StoreObject(string key, object value, TimeSpan expiry)
        {
            StoreObject(key, value, expiry, _cacheDefaultDatabase);
        }

        public void StoreObject(string key, object value, TimeSpan expiry, int database)
        {
            try
            {
                if (value == null)
                    return;

                var db = GetDatabase(database);
                var obj = JsonConvert.SerializeObject(value);
                db.StringSet(key, obj, expiry);
            }
            catch (Exception ex)
            {

            }
        }

        private IDatabase GetDatabase()
        {
            return _connectionMultiplexer.GetDatabase(_cacheDefaultDatabase);
        }

        private IDatabase GetDatabase(int database)
        {
            return _connectionMultiplexer.GetDatabase(database);
        }

        public void FlushEspecificDatabase()
        {
            FlushEspecificDatabase(_cacheDefaultDatabase);
        }

        public void FlushEspecificDatabase(int database)
        {
            var endpoints = OpenRedis().GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                server.FlushDatabase(database);
            }
        }

        public string GetString(string key)
        {
            return GetString(key, _cacheDefaultDatabase);
        }

        public string GetString(string key, int database)
        {
            try
            {
                return GetRedis(key, database);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await KeyDeleteAsync(key, _cacheDefaultDatabase);
        }

        public async Task<bool> KeyDeleteAsync(string key, int database)
        {
            var db = GetDatabase(database);

            return await db.KeyDeleteAsync(key);
        }

        public bool KeyDelete(string key)
        {
            return KeyDelete(key, _cacheDefaultDatabase);
        }

        public bool KeyDelete(string key, int database)
        {
            var db = GetDatabase(database);

            return db.KeyDelete(key, CommandFlags.HighPriority);
        }
    }
}
