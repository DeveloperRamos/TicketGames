using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketGames.CrossCutting.CrossCutting.Settings
{
    public interface ISettingsProvider
    {
        NameValueCollection AppSettings { get; }
        ConnectionStringSettings GetConnectionString(string name);
    }


    public class ConfigurationManagerSettingsProvider : ISettingsProvider
    {
        public NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        public ConnectionStringSettings GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }
    }

    public static class SettingsManager
    {
        private static ISettingsProvider _instance;
        public static ISettingsProvider Provider
        {
            get
            {
                if (_instance == null)
                    _instance = new ConfigurationManagerSettingsProvider();

                return _instance;
            }
        }
        public static void SetProvider(ISettingsProvider instance)
        {
            if (instance == null)
                throw new ArgumentException("Use \"ConfigurationManagetSettingsProvider\" instead of a null reference.");
            _instance = instance;
        }

        public static string GetString(string key)
        {
            return Provider.AppSettings.GetString(key);
        }
        public static int GetInt32(string key, int defaultValue = 0)
        {
            return Provider.AppSettings.GetInt32(key, defaultValue);
        }
        public static float GetFloat(string key, float defaultValue = 0)
        {
            var value = Provider.AppSettings.GetString(key);
            float ret;
            if (float.TryParse(value, out ret))
                return ret;

            return defaultValue;
        }

        public static string GetString(this NameValueCollection collection, string name, string defaultValue = null)
        {
            var value = collection[name];
            if (value != null)
                return value;

            return defaultValue;
        }
        public static int GetInt32(this NameValueCollection collection, string name, int defaultValue = 0)
        {
            var value = collection[name];
            int ret;
            if (int.TryParse(value, out ret))
                return ret;

            return defaultValue;
        }
        public static bool GetBool(this NameValueCollection collection, string name, bool defaultValue = false)
        {
            var value = collection[name];
            bool ret;
            if (bool.TryParse(value, out ret))
                return ret;
            return defaultValue;
        }
    }


    public class AppSettings
    {
        public class Parallels
        {
            public Parallels()
            {
            }

            /// <summary>
            /// Enable the Parallel.ForEach to consume 50% of CPU bound 
            /// </summary>
            public static int MaxDegreeOfParallelism_50 { get; internal set; } = 2;

            /// <summary>
            /// Enable the Parallel.ForEach to consume 100% of CPU bound 
            /// </summary>
            public static int MaxDegreeOfParallelism_100 { get; internal set; } = 4;
        }
    }

}
