using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WinDo.Utilities
{
    /// <summary>
    ///反射助手 
    /// </summary>
    public class ReflectHelper
    {
        private ReflectHelper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static BindingFlags bf = BindingFlags.DeclaredOnly | BindingFlags.Public |
                                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            object objReturn = null;
            Type type = obj.GetType();
            objReturn = type.InvokeMember(methodName, bf | BindingFlags.InvokeMethod, null, obj, args);
            return objReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetField(object obj, string name, object value)
        {
            FieldInfo fi = obj.GetType().GetField(name, bf);
            fi.SetValue(obj, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetField(object obj, string name)
        {
            FieldInfo fi = obj.GetType().GetField(name, bf);
            return fi.GetValue(obj);
        }

        /// <summary>
        /// 设置对象属性的值
        /// </summary>
        public static void SetProperty(object obj, string name, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bf);
            object objValue = ChangeType2(value, propertyInfo.PropertyType);
            propertyInfo.SetValue(obj, objValue, null);
        }
        public static object ChangeType2(object value, Type conversionType)
        {
            if (value is DBNull || value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 获取对象属性的值
        /// </summary>
        public static object GetProperty(object obj, string name)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bf);
            return propertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// 获取对象属性信息（组装成字符串输出）
        /// </summary>
        public static List<string> GetPropertyNames(object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
            return propertyInfos.Select(property => property.Name).ToList();
        }

        /// <summary>
        /// 获取对象属性信息（组装成字符串输出）
        /// </summary>
        public static Dictionary<string, string> GetPropertyNameTypes(object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
            return propertyInfos.ToDictionary(property => property.Name, property => property.PropertyType.FullName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSource"></param>
        /// <returns></returns>
        public static DataTable CreateTable(object objSource)
        {
            DataTable table = null;
            var objList = objSource as IEnumerable;
            foreach (object obj in objList)
            {
                if (table == null)
                {
                    List<string> nameList = ReflectHelper.GetPropertyNames(obj);
                    table = new DataTable("");
                    DataColumn column;

                    foreach (string name in nameList)
                    {
                        column = new DataColumn
                        {
                            DataType = System.Type.GetType("System.String"),
                            ColumnName = name,
                            Caption = name
                        };
                        table.Columns.Add(column);
                    }
                }

                DataRow row = table.NewRow();
                PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
                foreach (PropertyInfo property in propertyInfos)
                {
                    row[property.Name] = property.GetValue(obj, null);
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
