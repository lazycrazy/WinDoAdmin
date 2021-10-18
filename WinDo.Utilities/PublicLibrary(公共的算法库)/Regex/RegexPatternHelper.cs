
namespace WinDo.Utilities
{
    /// <summary>
    /// 正则定义
    /// </summary>
    public class RegexPatternHelper
    {
        /// <summary>
        /// 用户名 [A-Za-z\d\-\_]+
        /// </summary>
        public const string UserName = @"^[A-Za-z\d\-_]+$";
        /// <summary>
        /// EMail地址，如 huacnlee@gmail.com
        /// </summary>
        public const string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        /// <summary>
        /// URL地址
        /// </summary>
        public const string URL = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        /// <summary>
        /// 中文
        /// </summary>
        public const string Chinese = @"^[\u4e00-\u9fa5]+$";
        /// <summary>
        /// 整型，正数负数
        /// </summary>
        public const string Integer = @"^-?\d{1,9}$";
        /// <summary>
        /// 符点数
        /// </summary>
        public const string Float = @"^(-?\d+)(\.\d+)?$";
        /// <summary>
        /// 日期格式 支持1988-01-04 这种格式
        /// </summary>
        public const string Date = @"^[\d]{4}(-|/)[\d]{1,2}(-|/)[\d]{1,2}$";
        /// <summary>
        /// HTML 带 <> 的标签
        /// </summary>
        public const string HTMLTag = @"<.+?>";
        /// <summary>
        /// 18位身份证
        /// </summary>
        public const string IDCardNumber18 = @"^\d{15}|\d{17}[X|x]$";
        /// <summary>
        /// 15位身份证
        /// </summary>
        public const string IDCardNumber15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";
        /// <summary>
        /// IPv4的地址
        /// </summary>
        public const string IPAddress = @"\b((25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\.){3}(25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)\b$";
    }
}
