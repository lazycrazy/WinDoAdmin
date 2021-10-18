using System.Text.RegularExpressions;

namespace WinDo.Utilities
{
    /// <summary>
    /// 数字通用操作类  
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// 检测是否浮点型数据
        /// </summary>
        /// <param name="s">待检查数据</param>
        /// <returns>True:是浮点型，False:不是浮点型</returns>
        public static bool IsDecimal(string s)
        {
            try
            {
                decimal d = decimal.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="input">待检查数据</param>
        /// <returns></returns>
        public static bool IsInteger(string input)
        {
            return input != null && IsInteger(input, true);
        }

        /// <summary>
        /// 将输入的字符串转换成整数型数据
        /// </summary>
        /// <param name="strValue">待转换数据</param>
        /// <returns>成功，返回整数型数据；失败，返回null</returns>
        public static int? ToInteger(string strValue)
        {
            if (string.IsNullOrEmpty(strValue.Trim()))
            {
                return null;
            }

            try
            {
                return int.Parse(strValue.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static int? StringCastToInteger(object objValue)
        {
            if (objValue == null)
            {
                return null;
            }


            if (string.IsNullOrEmpty(objValue.ToString().Trim()))
            {
                return null;
            }
            try
            {
                return int.Parse(objValue.ToString().Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将输入的字符串转换成浮点型数据
        /// </summary>
        /// <param name="strValue">待转换数据</param>
        /// <returns>成功，返回浮点型数据；失败，返回null</returns>
        public static decimal? ToDecimal(string strValue)
        {
            if (string.IsNullOrEmpty(strValue.Trim()))
            {
                return null;
            }
            try
            {
                return decimal.Parse(strValue.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="input"></param>
        /// <param name="plus"></param>
        /// <returns></returns>
        public static bool IsInteger(string input, bool plus)
        {
            if (input == null)
            {
                return false;
            }
            string pattern = "^-?[0-9]+$";
            if (plus)
                pattern = "^[0-9]+$";
            return Regex.Match(input, pattern, RegexOptions.Compiled).Success;
        }
        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="s">要验证的字符串</param>
        /// <param name="precision">整数位有1到几位数字</param>
        /// <param name="scale">小数位有几位数字</param>
        /// <returns></returns>
        public static bool IsNumber(string s, int precision, int scale)
        {
            if ((precision == 0) && (scale == 0))
            {
                return false;
            }
            string pattern = @"(^\d{1," + precision + "}";
            if (scale > 0)
            {
                pattern += @"\.\d{0," + scale + "}$)|" + pattern;
            }
            pattern += "$)";
            return Regex.IsMatch(s, pattern);
        }
    }
}
