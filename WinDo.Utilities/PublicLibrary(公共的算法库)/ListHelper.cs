using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WinDo.Utilities
{
    public class ListHelper
    {
        /// <summary>
        /// 复制List对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="List">源对象</param>
        /// <returns>复制后的对象列表</returns>
        public static List<T> ListClone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }

        /// <summary>
        /// 得到一个对象的克隆(二进制的序列化和反序列化)--需要标记可序列化
        /// </summary>
        public static object Clone(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            return formatter.Deserialize(memoryStream);
        }

        public static object ListClone1(object ldc)
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, ldc);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
    }
}
