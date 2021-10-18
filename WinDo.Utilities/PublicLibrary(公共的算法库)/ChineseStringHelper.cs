using System;
using Microsoft.VisualBasic;

namespace WinDo.Utilities
{
    /// <summary>
    /// 简体与繁体相互转换
    /// </summary>
    public class ChineseStringHelper
    {
        /// <summary>
        /// 简体与繁体相互转换
        /// </summary>
        /// <param name="content">待转换的内容</param>
        /// <param name="type">
        /// 类型：
        ///     1：简体转繁体
        ///     2：繁体转简体
        /// </param>
        /// <returns></returns>
        public static string StringConvert(string content, int type)
        {
            String value = content;
            switch (type)
            {
                case 1://转繁体
                    value = Strings.StrConv(content, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
                    break;
                case 2:
                    value = Strings.StrConv(content, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
                    break;
                default:
                    break;
            }
            return value;
        }

    }
}
