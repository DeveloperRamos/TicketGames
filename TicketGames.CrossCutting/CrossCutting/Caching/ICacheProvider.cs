using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.CrossCutting.CrossCutting.Caching
{
    public interface ICacheProvider
    {
        object GetObject(Type type, string key);

        Task<object> GetObjectAsync(Type type, string key);

        T GetObject<T>(string key);

        T GetObject<T>(string key, int database);

        string GetString(string key);

        string GetString(string key, int database);

        T GetHashObject<T>(string key, string subKey);

        T GetHashObject<T>(string key, string subKey, int database);

        string GetHashObject(string key, string subKey);

        string GetHashObject(string key, string subKey, int database);

        Task<T> GetObjectAsync<T>(string key);

        void StoreObject(string key, object value, TimeSpan expiry);

        void StoreObject(string key, object value, TimeSpan expiry, int database);

        Task StoreObjectAsync(string key, object value, TimeSpan expiry);

        void FlushEspecificDatabase();

        void FlushEspecificDatabase(int database);

        List<T> GetHashsObjects<T>(string key, List<string> subKey);

        List<T> GetHashsObjects<T>(string key, List<string> subKey, int database);

        void SetHashsObjects(string key, Dictionary<string, string> hashValues);

        void SetHashsObjects(string key, Dictionary<string, string> hashValues, int database);

        Task<bool> KeyDeleteAsync(string key);

        Task<bool> KeyDeleteAsync(string key, int database);

        bool KeyDelete(string key);

        bool KeyDelete(string key, int database);
    }
}
