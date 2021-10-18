
using System;
using System.Reflection;

namespace WinDo.Utilities
{
    /// <summary>
    /// EnumDescription
    /// 枚举状态的说明。
    /// 
   
    public static class EnumExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum enumeration) 
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if (null != attrs && attrs.Length > 0)
                {
                    return ((EnumDescription)attrs[0]).Text;
                }
            }
            return enumeration.ToString(); 
        }
    }
}