using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TicketGames.CrossCutting
{
    public static class ExtensionMethods
    {
        static Dictionary<string, Type> s_Types = new Dictionary<string, Type>();
        /// <summary>
        /// Valida, baseado na exceção disparada, se corresponde a uma exceção de timeout, que pode corresponder a diferentes tipos de exceção.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static bool IsTimeoutException(this System.Exception exception)
        {
            if (exception.GetType() == typeof(AggregateException))
            {
                AggregateException aggregateException = (AggregateException)exception;
                if (aggregateException.InnerExceptions.Any(e => e.GetType() == typeof(TaskCanceledException)))
                    return true;
            }

            if (exception.GetType() == typeof(TimeoutException))
                return true;

            if (exception.GetType() == typeof(System.Net.WebException))
            {
                System.Net.WebException webException = (System.Net.WebException)exception;
                if (webException.Status == System.Net.WebExceptionStatus.Timeout)
                    return true;
            }
            return false;
        }
        public static TBaseType CreateInstance<TBaseType>(this AppDomain appDomain, Guid guid, params object[] parameters)
        {
            Type type;
            var baseType = typeof(TBaseType);
            string key = baseType.FullName + guid;

            if (s_Types.TryGetValue(key, out type))
                return (TBaseType)Activator.CreateInstance(type, parameters);

            lock (s_Types)
            {
                if (s_Types.TryGetValue(key, out type))
                    return (TBaseType)Activator.CreateInstance(type, parameters);

                var types = appDomain.GetAssemblies().OrderBy(i => i.FullName.StartsWith("LTM", StringComparison.InvariantCultureIgnoreCase) ? 0 : 1)
                                        .SelectMany(i => i.GetTypes())
                                        .Where(i => i.GetCustomAttribute<GuidAttribute>() != null && Guid.Parse(i.GetCustomAttribute<GuidAttribute>().Value) == guid);

                if (baseType.IsInterface)
                    type = types.Where(i => i.GetInterfaces().Any(intf => intf == baseType)).FirstOrDefault();
                else
                    type = types.Where(i => i.IsInstanceOfType(baseType)).FirstOrDefault();

                if (type == null)
                    throw new InvalidOperationException(string.Format("Could not find type '{0}' with Guid '{1}'", baseType.FullName, guid));

                s_Types.Add(key, type);
                return (TBaseType)Activator.CreateInstance(type, parameters);
            }
        }

        public static bool IsNumeric(this string value)
        {
            int result = default(int);
            return int.TryParse(value, out result);
        }
        public static bool IsLong(this string value)
        {
            long result = default(long);
            return long.TryParse(value, out result);
        }

        public static bool IsDecimal(this string value)
        {
            decimal result = default(decimal);
            return decimal.TryParse(value, out result);
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static T GetCustomAttribute<T>(this Enum enumValue) where T : System.Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDescription(this Enum enumValue)
        {
            var attr = enumValue.GetCustomAttribute<DescriptionAttribute>();
            if (attr == null)
                return null;

            return attr.Description;
        }

        public static TEnum ToEnum<TEnum>(this Enum value)
        {
            return (TEnum)Convert.ChangeType(value, typeof(int));
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.Add(item);
        }

        public static string GetString(this NameValueCollection collection, string name, string defaultValue = null)
        {
            var value = collection[name];
            if (value != null)
                return value;

            return defaultValue;
        }

        public static string GetStringNonNullOrEmpty(this NameValueCollection collection, string name)
        {
            var value = collection[name];
            if (value != null)
                return value;
            throw new KeyNotFoundException("The key \"" + name + "\" is null or empty");
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

        public static string GetInnerMessages(this Exception ex)
        {
            if (ex.InnerException == null)
                return ex.Message;

            var sb = new StringBuilder();
            do
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                if (ex.InnerException != null && char.IsLetterOrDigit(ex.Message.Last()))
                    sb.AppendLine(":");
                ex = ex.InnerException;
                if (ex != null)
                    sb.AppendLine();
            }
            while (ex != null);

            return sb.ToString();
        }

        public static string GetInnerMessagesWithTrace(this Exception ex)
        {
            if (ex.InnerException == null)
                return string.Concat(ex.Message, " ", ex.ToString());

            var sb = new StringBuilder();
            do
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                if (ex.InnerException != null && char.IsLetterOrDigit(ex.Message.Last()))
                    sb.AppendLine(":");
                ex = ex.InnerException;
                if (ex != null)
                    sb.AppendLine();
            }
            while (ex != null);

            return sb.ToString();
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (TextReader tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }

        public static bool EqualsObject(object objInitial, object objCompare)
        {
            var lRetornoAlteracao = new Dictionary<string, string>();
            objInitial.GetType().FindMembers(MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance,
                Type.FilterName, "*").ToList().ForEach(p =>
                {
                    try
                    {
                        var valorInicial = ((PropertyInfo)p).GetValue(objInitial);
                        var valorNovo = objCompare.GetType().GetProperty(p.Name).GetValue(objCompare);

                        if (valorInicial.GetType().UnderlyingSystemType.IsGenericType)
                        {
                            var index = 0;
                            dynamic vInicialList = valorInicial;
                            foreach (dynamic it in vInicialList)
                            {
                                var iAlt = ((dynamic)valorNovo)[index];
                                var ret = EqualsObject(it, iAlt);
                                foreach (KeyValuePair<string, string> item in ret)
                                    lRetornoAlteracao.Add(string.Concat(item.Key, "(", index.ToString(), ")"),
                                        item.Value);
                                index++;
                            }
                        }
                        else

                            if (!valorInicial.Equals(valorNovo))
                        {
                            lRetornoAlteracao.Add(p.DeclaringType.FullName, valorNovo.ToString());
                        }
                    }

                    catch (NullReferenceException nex)
                    {
                        lRetornoAlteracao.Add(objInitial.GetType().Name, string.Concat(" Exception - Objeto nulo ", nex.Source));
                    }
                    catch (Exception ex)
                    {
                        lRetornoAlteracao.Add(objInitial.GetType().Name, string.Concat(" Exception ", ex.Source));
                    }
                });

            if (lRetornoAlteracao.Count > 0)
                return false;
            else
                return true;
        }
    }
}
