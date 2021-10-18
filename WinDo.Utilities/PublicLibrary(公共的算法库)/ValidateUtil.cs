using System.IO;
using System.Text.RegularExpressions;

namespace WinDo.Utilities
{
    /// <summary>
    /// ValidateUtil
    /// 常用验证公共类   
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>
        /// 检查文件夹名称
        /// </summary>
        /// <param name="folderName">文件夹名称</param>
        /// <returns></returns>
        public static bool CheckFolderName(string folderName)
        {
            bool returnValue = true;
            if (folderName.Trim().Length == 0)
            {
                returnValue = false;
            }
            else
            {
                if ((folderName.IndexOfAny(Path.GetInvalidPathChars()) >= 0) || (folderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0))
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// 密码强度检查
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>检查通过</returns>
        public static bool EnableCheckPasswordStrength(string password)
        {
            bool returnValue = !string.IsNullOrEmpty(password);
            bool isDigit = false;
            bool isLetter = false;
            if (password != null)
            {
                foreach (char t in password)
                {
                    if (!isDigit)
                    {
                        isDigit = char.IsDigit(t);
                    }

                    if (!isLetter)
                    {
                        isLetter = char.IsLetter(t);
                    }
                }
                returnValue = (isDigit && isLetter);
                // 密码至少为6位，为数字加字母
                if (password.Length < 6)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        
        /// <summary>
        /// 检查文件名称
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static bool CheckFileName(string fileName)
        {
            bool returnValue = true;
            if (fileName.Trim().Length == 0)
            {
                returnValue = false;
            }
            else
            {
                if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        /// <summary>是否空</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsBlank(string strInput)
        {
            return string.IsNullOrEmpty(strInput);
        }

        /// <summary>是否数字</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
                return false;
            var reg = new Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(strInput);
        }

        /// <summary>
        /// 是不是时间类型数据
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strDate)
        {
            //TODO:此正则有问题，有待修改完善。
            var reg =
                new Regex(
                    @"(((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9]))|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29))|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)))((\s+(0?[1-9]|1[012])(:[0-5]\d){0,2}(\s[AP]M))?$|(\s+([01]\d|2[0-3])(:[0-5]\d){0,2})?$))");
            return reg.IsMatch(strDate);
        }

        /// <summary>
        /// 检查邮件是否何法
        /// </summary>
        /// <param name="inputEmail">输入的电子邮箱</param>
        /// <returns></returns>
        public static bool IsEmail(string inputEmail)
        {
            const string regexString =
                @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            var regex = new Regex(regexString);
            return regex.IsMatch(inputEmail.Trim());
        }


        public static bool CheckEmail(string email)
        {
            bool returnValue = true;
            if (email.Trim().Length == 0)
            {
                // 先按可以为空
                returnValue = true;
            }
            else
            {
                Regex Regex = new Regex("[\\w-]+@([\\w-]+\\.)+[\\w-]+");
                if (!Regex.IsMatch(email))
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
    }
}