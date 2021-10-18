using System;
using System.Text.RegularExpressions;

namespace WinDo.Utilities
{
    /// <summary>
    /// 利用正则表达式来验证各种字符串格式
    /// </summary>
    public class RegexValidatorHelper
    {
        //private static string Regex_Account = @"^[A-Za-z0-9]+$";
        private static string Regex_Account = @"^\w+$";
        //符号+数字+字母  密码
        private static string Regex_Password = @"^([A-Za-z0-9\~\!\@\#\$\%\^\&\*\(\)_\+\|\`\-\=\\\<\>\?\,\.\/\:\;\{\}\[\]]){6,}$";
        //private static string Regex_Password = @"^[A-Za-z0-9]+$";
        private static string Regex_Email = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        private static string Regex_Url = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        private static string Regex_TrackUrl = @"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        private static string Regex_Num_Char_Underline = @"^\w+$";
        private static string Regex_Char = @"^[A-Za-z]+$";
        private static string Regex_SmallChar = @"^[a-z]+$";
        private static string Regex_BigChar = @"^[A-Z]+$";
        private static string Regex_Integer = @"^-?\d{1,9}$";
        private static string Regex_Positive_Integer = @"^[0-9]*[1-9][0-9]*$";
        private static string Regex_Negative_Integer = @"^-[0-9]*[1-9][0-9]*$";
        private static string Regex_Nonpositive_Integer = @"^\d+$";
        private static string Regex_Nonnegative_Integer = @"^((-\d+)|(0+))$";
        private static string Regex_Float = @"^(-?\d+)(\.\d+)?$";
        private static string Regex_Positive_Float = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
        private static string Regex_Negative_Float = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
        private static string Regex_Nonpositive_Float = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
        private static string Regex_Nonnegative_Float = @"^\d+(\.\d+)?$";
        private static string Regex_Chinese = @"^[\u4e00-\u9fa5]+$";
        private static string Regex_Double_Byte = @"[^\x00-\xff]+$";
        private static string Regex_ReSendEmail = @"^[\d]+:[\w]{32}$";
        private static string Regex_ValidateCode = @"^[\w]{32}$";
        private static string Regex_Number_TwoPoints = @"^\d+(?:\.\d{0,2})?$";
        private static string Regex_Mobile = @"^13[\d]{9}$";// ^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[089]\d{8}|18[89]\d{8}
        private static string Regex_Phone = @"^([\d]{3,5}-)?[\d]{7,8}$";
        private static string Regex_QQ = @"^[1-9][0-9]{4,}$";
        private static string Regex_ZIP = @"^[1-9]\d{5}(?!\d)$";
        private static string Regex_FileName = @"^[a-zA-Z0-9]+.(aspx|Aspx|aSpx|asPx|aspX|ASpx|AsPx|AspX|aSpX|aSPx|asPX|aSpX|ASPx|ASPX|ASpX|aSPX|AsPX|aSPX)$";
        private static string Regex_DateTime = @"^[\d]{4}(-|/)[\d]{1,2}(-|/)[\d]{1,2}$";
        private static string Regex_NickName = @"^[A-Za-z0-9\u0391-\uFFE5]{1,20}$";
        private static string Regex_UserLoves = @"^(,\d+)+$";
        private static string Regex_SpaceName = @"^[a-zA-Z0-9]{3,30}$";
        private static string Regex_Emails = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+(,[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+)*$";
        private static string Regex_HtmlLable = @"<.+?>";
        private static string Regex_ProjectTag = @"^[\w\u0391-\uFFE5]+(,[\w\u0391-\uFFE5]+){0,20}$";
        private static string Regex_ShareID = @"^[\d]{1,9}(\|[\d]{1,9})*$";
        private static string Regex_AreaName = @"^[\w\u0391-\uFFE5]+(;[\w\u0391-\uFFE5]+){0,10}$";
        private static string Regex_SingleAreaName = @"^[\w\u0391-\uFFE5]{0,10}$";
        private static string Regex_CommendSight = @"^[\d]+(\|[\d]+){0,19}$";
        private static string Regex_OutDoorProject = @"^[\d]+,[\w\u0391-\uFFE5]+$";
        private static string Regex_IDCardNumber18 = @"^\d{15}|\d{17}[X|x]$";
        private static string Regex_IDCardNumber15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";

        /// <summary>
        /// 验证一个字符串格式是否符合某个正则表达式的内容
        /// </summary>
        /// <param name="input">待验证的字符串</param>
        /// <param name="pattern">正则表达式的种类，枚举型，可选择</param>
        /// <returns>
        /// 验证通过，返回真
        /// 验证未通过，返回假
        /// </returns>
        public static bool IsMatch(string input, Pattern pattern)
        {
            bool isMatch = false;
            if (string.IsNullOrEmpty(input))
                return isMatch;

            switch (pattern)
            {
                case Pattern.ACCOUNT:
                    isMatch = Regex.IsMatch(input, Regex_Account);
                    break;
                case Pattern.PASSWORD:
                    isMatch = Regex.IsMatch(input, Regex_Password);
                    break;
                case Pattern.EMAIL:
                    isMatch = Regex.IsMatch(input, Regex_Email);
                    break;
                case Pattern.URL:
                    isMatch = Regex.IsMatch(input, Regex_Url);
                    break;
                case Pattern.NUM_CHAR_UNDERLINE:
                    isMatch = Regex.IsMatch(input, Regex_Num_Char_Underline);
                    break;
                case Pattern.CHAR:
                    isMatch = Regex.IsMatch(input, Regex_Char);
                    break;
                case Pattern.SMALLCHAR:
                    isMatch = Regex.IsMatch(input, Regex_SmallChar);
                    break;
                case Pattern.BIGCHAR:
                    isMatch = Regex.IsMatch(input, Regex_BigChar);
                    break;
                case Pattern.INTEGER:
                    isMatch = Regex.IsMatch(input, Regex_Integer);
                    break;
                case Pattern.POSITIVE_INTEGER:
                    isMatch = Regex.IsMatch(input, Regex_Positive_Integer);
                    break;
                case Pattern.NEGATIVE_INTEGER:
                    isMatch = Regex.IsMatch(input, Regex_Negative_Integer);
                    break;
                case Pattern.NONPOSITIVE_INTEGER:
                    isMatch = Regex.IsMatch(input, Regex_Nonpositive_Integer);
                    break;
                case Pattern.NONNEGATIVE_INTEGER:
                    isMatch = Regex.IsMatch(input, Regex_Nonnegative_Integer);
                    break;
                case Pattern.FLOAT:
                    isMatch = Regex.IsMatch(input, Regex_Float);
                    break;
                case Pattern.POSITIVE_FLOAT:
                    isMatch = Regex.IsMatch(input, Regex_Positive_Float);
                    break;
                case Pattern.NEGATIVE_FLOAT:
                    isMatch = Regex.IsMatch(input, Regex_Negative_Float);
                    break;
                case Pattern.NONPOSITIVE_FLOAT:
                    isMatch = Regex.IsMatch(input, Regex_Nonpositive_Float);
                    break;
                case Pattern.NONNEGATIVE_FLOAT:
                    isMatch = Regex.IsMatch(input, Regex_Nonnegative_Float);
                    break;
                case Pattern.CHINESE:
                    isMatch = Regex.IsMatch(input, Regex_Chinese);
                    break;
                case Pattern.DOUBLE_BYTE:
                    isMatch = Regex.IsMatch(input, Regex_Double_Byte);
                    break;
                case Pattern.RESENDEMAIL:
                    isMatch = Regex.IsMatch(input, Regex_ReSendEmail);
                    break;
                case Pattern.VALIDATECODE:
                    isMatch = Regex.IsMatch(input, Regex_ValidateCode);
                    break;
                case Pattern.NUMBERWITHTOWPOINTS:
                    isMatch = Regex.IsMatch(input, Regex_Number_TwoPoints);
                    break;
                case Pattern.MOBILEPHONE:
                    isMatch = Regex.IsMatch(input, Regex_Mobile);
                    break;
                case Pattern.GUDINGPHONE:
                    isMatch = Regex.IsMatch(input, Regex_Phone);
                    break;
                case Pattern.QQ:
                    isMatch = Regex.IsMatch(input, Regex_QQ);
                    break;
                case Pattern.ZIP:
                    isMatch = Regex.IsMatch(input, Regex_ZIP);
                    break;
                case Pattern.FILENAME:
                    isMatch = Regex.IsMatch(input, Regex_FileName);
                    break;
                case Pattern.DATETIME:
                    isMatch = Regex.IsMatch(input, Regex_DateTime);
                    break;
                case Pattern.NICKNAME:
                    isMatch = Regex.IsMatch(input, Regex_NickName);
                    break;
                case Pattern.USERLOVE:
                    isMatch = Regex.IsMatch(input, Regex_UserLoves);
                    break;
                case Pattern.SPACENAME:
                    isMatch = Regex.IsMatch(input, Regex_SpaceName);
                    break;
                case Pattern.EMAILS:
                    isMatch = Regex.IsMatch(input, Regex_Emails);
                    break;
                case Pattern.HTMLLABLE:
                    isMatch = Regex.IsMatch(input, Regex_HtmlLable);
                    break;
                case Pattern.PROJECTTAG:
                    isMatch = Regex.IsMatch(input, Regex_ProjectTag);
                    break;
                case Pattern.SHAREID:
                    isMatch = Regex.IsMatch(input, Regex_ShareID);
                    break;
                case Pattern.AREANAME:
                    isMatch = Regex.IsMatch(input, Regex_AreaName);
                    break;
                case Pattern.SINGLEAREANAME:
                    isMatch = Regex.IsMatch(input, Regex_SingleAreaName);
                    break;
                case Pattern.TRACKURL:
                    isMatch = Regex.IsMatch(input, Regex_TrackUrl);
                    break;
                case Pattern.SIGHT:
                    isMatch = Regex.IsMatch(input, Regex_CommendSight);
                    break;
                case Pattern.OUTDOOR:
                    isMatch = Regex.IsMatch(input, Regex_OutDoorProject);
                    break;
                case Pattern.IDCARDNUMBER18:
                    isMatch = Regex.IsMatch(input, Regex_IDCardNumber18);
                    break;
                case Pattern.IDCARDNUMBER15:
                    isMatch = Regex.IsMatch(input, Regex_IDCardNumber15);
                    break;
                default:
                    isMatch = false;
                    break;
            }

            return isMatch;
        }

        public static string GetBudget2BitBackDot(string budget)
        {
            Regex r = new Regex(@"^(?<budget>\d+.\d{1,2})$");
            try
            {
                return r.Match(budget).Result("${budget}"); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public enum Pattern : byte
    {
        /// <summary>
        /// 验证账号格式，由数字和26个英文字母组成的字符串
        /// </summary>
        ACCOUNT = 0,
        /// <summary>
        /// 验证密码格式,由数字和26个英文字母组成的字符串
        /// </summary>
        PASSWORD = 1,
        /// <summary>
        /// 验证Email格式
        /// </summary>
        EMAIL = 2,
        /// <summary>
        /// 验证Url地址格式
        /// </summary>
        URL = 3,
        /// <summary>
        /// 数字，26个大小写英文字母和下划线
        /// </summary>
        NUM_CHAR_UNDERLINE = 4,
        /// <summary>
        /// 26个大小写英文字母
        /// </summary>
        CHAR = 5,
        /// <summary>
        /// 26个小写字母
        /// </summary>
        SMALLCHAR = 6,
        /// <summary>
        /// 26个大写字母
        /// </summary>
        BIGCHAR = 7,
        /// <summary>
        /// 整数
        /// </summary>
        INTEGER = 8,
        /// <summary>
        /// 正整数
        /// </summary>
        POSITIVE_INTEGER = 9,
        /// <summary>
        /// 负整数
        /// </summary>
        NEGATIVE_INTEGER = 10,
        /// <summary>
        /// 非正整数
        /// </summary>
        NONPOSITIVE_INTEGER = 11,
        /// <summary>
        /// 非负整数
        /// </summary>
        NONNEGATIVE_INTEGER = 12,
        /// <summary>
        /// 浮点数
        /// </summary>
        FLOAT = 13,
        /// <summary>
        /// 正浮点数
        /// </summary>
        POSITIVE_FLOAT = 14,
        /// <summary>
        /// 负浮点数
        /// </summary>
        NEGATIVE_FLOAT = 15,
        /// <summary>
        /// 非正浮点数
        /// </summary>
        NONPOSITIVE_FLOAT = 16,
        /// <summary>
        /// 非负浮点数
        /// </summary>
        NONNEGATIVE_FLOAT = 17,
        /// <summary>
        /// 只允许中文字符
        /// </summary>
        CHINESE = 18,
        /// <summary>
        /// 只允许双字节字符，包含中文
        /// </summary>
        DOUBLE_BYTE = 19,

        /// <summary>
        /// 重新发送邮件
        /// </summary>
        RESENDEMAIL = 20,

        /// <summary>
        /// 验证验证码
        /// </summary>
        VALIDATECODE = 21,

        /// <summary>
        /// 精确到小数点后两位
        /// </summary>
        NUMBERWITHTOWPOINTS = 22,

        /// <summary>
        /// 手机号码
        /// </summary>
        MOBILEPHONE = 23,

        /// <summary>
        /// 固定电话
        /// </summary>
        GUDINGPHONE = 24,

        /// <summary>
        /// QQ验证
        /// </summary>
        QQ = 25,

        /// <summary>
        /// 邮政编码
        /// </summary>
        ZIP = 26,
        /// <summary>
        /// 文件名
        /// </summary>
        FILENAME = 27,

        /// <summary>
        /// 日期
        /// </summary>
        DATETIME = 28,

        /// <summary>
        /// 昵称
        /// </summary>
        NICKNAME = 29,

        /// <summary>
        /// 用户爱好
        /// </summary>
        USERLOVE = 30,

        /// <summary>
        /// 用户的空间名称
        /// </summary>
        SPACENAME = 31,

        /// <summary>
        /// 多个邮箱
        /// </summary>
        EMAILS = 32,

        /// <summary>
        /// HTML标签
        /// </summary>
        HTMLLABLE = 33,

        /// <summary>
        /// 计划TAG
        /// </summary>
        PROJECTTAG = 34,

        /// <summary>
        /// 共享时用|分割
        /// </summary>
        SHAREID = 35,

        /// <summary>
        /// 地区
        /// </summary>
        AREANAME = 36,

        /// <summary>
        /// 引用地址
        /// </summary>
        TRACKURL = 37,
        /// <summary>
        /// 推荐景点
        /// </summary>
        SIGHT = 38,
        SINGLEAREANAME = 39,
        /// <summary>
        /// 户外活动
        /// </summary>
        OUTDOOR = 40,

        /// <summary>
        /// 18位中国身份证
        /// </summary>
        IDCARDNUMBER18 = 41,

        /// <summary>
        /// 15位中国身份证
        /// </summary>
        IDCARDNUMBER15 = 42,
    }
}
