using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Reflection;
using System.Dynamic;

namespace WinDo.Utilities
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Extensions
    {
        public static bool HasProperty(this object data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>)data).ContainsKey(propertyname);
            return data.GetType().GetProperty(propertyname) != null;
        }
        public static void SetPropertyValue(this object obj, string propName, object value)
        {
            if (obj == null || obj.GetType().GetProperty(propName) == null)
            {
                return;
            }
            if (value == null)
            {
                obj.GetType().GetProperty(propName).SetValue(obj, value, null);
                return;
            }
            //find out the type
            Type type = obj.GetType();
            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propName);
            //find the property type
            Type propertyType = propertyInfo.PropertyType;
            //空字符串处理,字符串类型赋值，否则设置为null
            if (value.ToString().Trim() == string.Empty)
            {
                propertyInfo.SetValue(obj, (propertyType == typeof(string) ? value : null), null);
                return;
            }
            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            var v = Convert.ChangeType(value, targetType);

            //Set the value of the property
            propertyInfo.SetValue(obj, v, null);
        }
        public static object GetPropertyValue(this object obj, string propName)
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// dynamic to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }
        /// <summary>
        /// object to IDictionary
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }
        /// <summary>
        /// object to dynamic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static dynamic AsDynamic<T>(this T obj)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var currentValue = propertyInfo.GetValue(obj, null);
                expando.Add(propertyInfo.Name, currentValue);
            }
            return expando as ExpandoObject;
        }
        public static object DicToObj(Dictionary<string, object> additionalProperties)
        {
            var obj = (IDictionary<string, object>)(new System.Dynamic.ExpandoObject());
            foreach (var name in additionalProperties.Keys)
                obj[name] = additionalProperties[name];
            return obj;
        }

        public static string ToTitleCase(this string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var text = cultureInfo.TextInfo.ToLower(str);
            return cultureInfo.TextInfo.ToTitleCase(text);
        }


        /// <summary>
        /// Transform object into Identity data type (integer).
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is -1.</param>
        /// <returns>Identity value.</returns>
        public static int AsId(this object item, int defaultId = -1)
        {
            if (item == null)
                return defaultId;


            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultId;

            return result;
        }

        /// <summary>
        /// Transform object into integer data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(int).</param>
        /// <returns>The integer value.</returns>
        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        /// <summary>
        /// Transform object into integer data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(int).</param>
        /// <returns>The integer value.</returns>
        public static int? AsDefaultNullInt(this object item, int? defaultInt = null)
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        /// <summary>
        /// Transform object into double data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(double).</param>
        /// <returns>The double value.</returns>
        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }

        public static decimal AsDecimal(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            decimal result;
            if (!decimal.TryParse(item.ToString(), out result))
                return defaultDecimal;

            return result;
        }

        /// <summary>
        /// Transform object into string data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(string).</param>
        /// <returns>The string value.</returns>
        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }

        /// <summary>
        /// Transform object into DateTime data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(DateTime).</param>
        /// <returns>The DateTime value.</returns>
        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }

        public static DateTime? AsDefaultNullDateTime(this object item, DateTime? defaultDateTime = null)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }

        /// <summary>
        /// Transform object into bool data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(bool).</param>
        /// <returns>The bool value.</returns>
        public static bool AsBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return new List<string>() { "yes", "y", "true", "1", "１" }.Contains(item.ToString().ToLower());
        }

        /// <summary>
        /// Transform string into byte array.
        /// </summary>
        /// <param name="s">The object to be transformed.</param>
        /// <returns>The transformed byte array.</returns>
        public static byte[] AsByteArray(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return Convert.FromBase64String(s);
        }

        /// <summary>
        /// Transform object into base64 string.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <returns>The base64 string value.</returns>
        public static string AsBase64String(this object item)
        {
            if (item == null)
                return null;
            ;

            return Convert.ToBase64String((byte[])item);
        }

        /// <summary>
        /// Transform Binary into base64 string data type. 
        /// Note: This is used in LINQ to SQL only.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <returns>The base64 string value.</returns>
        public static string AsBase64String(this Binary item)
        {
            if (item == null)
                return null;

            return Convert.ToBase64String(item.ToArray());
        }

        /// <summary>
        /// Transform base64 string to Binary data type. 
        /// Note: This is used in LINQ to SQL only.
        /// </summary>
        /// <param name="s">The base 64 string to be transformed.</param>
        /// <returns>The Binary value.</returns>
        public static Binary AsBinary(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return new Binary(Convert.FromBase64String(s));
        }

        /// <summary>
        /// Transform object into Guid data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <returns>The Guid value.</returns>
        public static Guid AsGuid(this object item)
        {
            try { return new Guid(item.ToString()); }
            catch { return Guid.Empty; }
        }

        /// <summary>
        /// Concatenates SQL and ORDER BY clauses into a single string. 
        /// </summary>
        /// <param name="sql">The SQL string</param>
        /// <param name="sortExpression">The Sort Expression.</param>
        /// <returns>Contatenated SQL Statement.</returns>
        public static string OrderBy(this string sql, string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression))
                return sql;

            return sql + " ORDER BY " + sortExpression;
        }

        /// <summary>
        /// Takes an enumerable source and returns a comma separate string.
        /// Handy to build SQL Statements (for example with IN () statements) from object collections.
        /// </summary>
        /// <typeparam name="T">The Enumerable type</typeparam>
        /// <typeparam name="U">The original data type (typically identities - int).</typeparam>
        /// <param name="source">The enumerable input collection.</param>
        /// <param name="func">The function that extracts property value in object.</param>
        /// <returns>The comma separated string.</returns>
        public static string CommaSeparate<T, U>(this IEnumerable<T> source, Func<T, U> func, string separate = ",")
        {
            return string.Join(separate, source.Select(s => func(s).ToString()).ToArray());
        }
    }
}
