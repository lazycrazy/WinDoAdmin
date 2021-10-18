using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace WinDo.Utilities
{
    /// <summary>
    /// 字符串通用操作类  
    /// </summary>
    public static class StringHelper
    {

        /// <summary>
        /// 拆分字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            var count = (int)Math.Ceiling((double)str.Length / chunkSize);
            return Enumerable.Range(0, count)
                .Select(i => str.Substring(i * chunkSize, (i * chunkSize + chunkSize <= str.Length) ? chunkSize : str.Length - i * chunkSize));

        }

        public static IEnumerable<IEnumerable<TSource>> ChunkBy<TSource>(this IEnumerable<TSource> source, int chunkSize)
        {
            while (source.Any())                     // while there are elements left
            {   // still something to chunk:
                yield return source.Take(chunkSize); // return a chunk of chunkSize
                source = source.Skip(chunkSize);     // skip the returned chunk
            }
        }
        /// <summary>
        /// 替换一次
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="str">所要截取的字符串</param>
        /// <param name="num">截取字符串的长度</param>
        /// <returns></returns>
        public static string GetSubString(string str, int num)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            string outstr = "";
            int n = 0;
            foreach (char ch in str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > num)
                    break;
                outstr += ch;
            }
            return outstr;
        }
        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="str">所要截取的字符串</param>
        /// <param name="num">截取字符串的长度</param>
        /// <param name="lastStr">截取字符串后省略部分的字符串</param>
        /// <returns></returns>
        public static string GetSubString(string str, int num, string lastStr)
        {
            return (str.Length > num) ? str.Substring(0, num) + lastStr : str;
        }

        /// <summary>
        /// 验证字符串是否是图片路径
        /// </summary>
        /// <param name="input">待检测的字符串</param>
        /// <returns>返回true 或 false</returns>
        public static bool IsImgString(string input)
        {
            return IsImgString(input, "/{@dirfile}/");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="checkStr"></param>
        /// <returns></returns>
        public static bool IsImgString(string input, string checkStr)
        {
            bool re_Val = false;
            if (input != string.Empty)
            {
                string s_input = input.ToLower();
                if (s_input.IndexOf(checkStr.ToLower(), System.StringComparison.Ordinal) != -1 && s_input.IndexOf(".", System.StringComparison.Ordinal) != -1)
                {
                    string Ex_Name = s_input.Substring(s_input.LastIndexOf(".", System.StringComparison.Ordinal) + 1).ToString().ToLower();
                    if (Ex_Name == "jpg" || Ex_Name == "gif" || Ex_Name == "bmp" || Ex_Name == "png")
                    {
                        re_Val = true;
                    }
                }
            }
            return re_Val;
        }


        /// <summary>
        /// 检测含中文字符串实际长度
        /// </summary>
        /// <param name="input">待检测的字符串</param>
        /// <returns>返回正整数</returns>
        public static int NumChar(string input)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(input);
            int l = 0;
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)//判断是否为汉字或全脚符号
                {
                    l++;
                }
                l++;
            }
            return l;
        }

        /// <summary>
        /// 判断是否有危险字符
        /// </summary>
        /// <param name="word">输入字符串</param>
        /// <returns>True:存在危险字符;False：无危险字符</returns>
        public static bool HasDangerousWord(string word)
        {
            bool bFlag = false;
            string[] DangerouWord = new string[] {
                                                     "delete", "truncate", "drop", "insert"
                                                    ,"update", "exec","select","truncate"
                                                    ,"dbcc","@","alter","drop","create","if"
                                                    ,"else","and","add","open","return"
                                                    ,"exists","declare","go","use"
                                                  };

            word = word.ToLower();
            int iCount = DangerouWord.Length;
            for (int i = 0; i < iCount; i++)
            {
                if (word.Contains(DangerouWord[i]))
                {
                    bFlag = true;
                    break;
                }
            }

            return bFlag;
        }

        /// <summary>
        /// 按指定(字节)长度截取字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>string</returns>
        public static string CutStringByte(string str, int length, bool addDot = true)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (System.Text.Encoding.Default.GetByteCount(str) < length)
            {
                return str;
            }
            int i = 0;//字节数
            int j = 0;//实际截取长度
            foreach (char newChar in str)
            {
                if ((int)newChar > 127)
                {
                    //汉字
                    i += 2;
                }
                else
                {
                    i++;
                }

                if (i < length)
                    j++;
                else
                    break;
            }
            str = str.Substring(0, j);
            if (addDot)
                str += "...";
            return str;
        }

        /// <summary>
        /// 移除最后的字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveFinalChar(string s)
        {
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            return s;
        }

        /// <summary>
        /// 移除最后的逗号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveFinalComma(string s)
        {
            if (s.Trim().Length > 0)
            {
                int c = s.LastIndexOf(",");
                if (c > 0)
                {
                    s = s.Substring(0, s.Length - (s.Length - c));
                }
            }
            return s;
        }

        /// <summary>
        /// 移除字符中的空格
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveSpaces(string s)
        {
            s = s.Trim();
            s = s.Replace(" ", "");
            return s;
        }

        /// <summary>
        /// 判断字符是否NULL或者为空
        /// </summary>
        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 去除字符串最后一个','号
        /// </summary>
        /// <param name="chr">:要做处理的字符串</param>
        /// <returns>返回已处理的字符串</returns>
        /// /// CreateTime:2007-03-26 Code By DengXi
        public static string Lost(string chr)
        {
            if (string.IsNullOrEmpty(chr))
            {
                return "";
            }
            chr = chr.Remove(chr.LastIndexOf(","));
            return chr;
        }

        /// <summary> 
        /// 汉字转拼音缩写 
        /// （建议使用：ToChineseSpell方法）
        /// </summary> 
        /// <param name="input">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string GetPYString(string input)
        {
            return input.Aggregate("", (current, c) => current + ((int)c >= 33 && (int)c <= 126 ? c.ToString() : GetPYChar(c.ToString())));
        }

        /// <summary> 
        /// 取单个字符的拼音声母 
        ///
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "G";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }

        /// <summary>
        ///思路非常简单,且没有任何位数限制! 
        ///例如: 401,0103,1013 
        ///读作: 肆佰零壹[亿]零壹佰零叁[万]壹仟零壹拾叁 
        ///咱们先按每四位一组 从左到右,高位到低位分别"大声朗读"一下: 
        ///"肆佰零壹" 单位是: "[亿]" 
        ///"壹佰零叁" 单位是: "[万]" 
        ///"壹仟零壹拾叁" 单位是 "" (相当于没有单位) 
        ///很容易发现,每四位: 只有 千位,百位,十位,个位 这四种情况! 
        ///我们把 [万],[亿] 当作单位就可以了! 
        ///这就是规律了!简单吧! 
        ///依据该思路,只用区区不到 50 行代码就可以搞定: 
        ///只要你能够提供足够多的"单位" 
        ///任何天文数字都可以正确转换! 
        /// </summary>
        /// <param name="num">阿拉伯数字</param>
        /// <returns>返回格式化好的字符串</returns>
        internal static string ConvertNumberToChinese(string num)
        {
            //数字 数组 
            string[] cnNum = new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            //位 数组 
            string[] cnSBQ = new string[] { "", "拾", "佰", "仟" };
            //单位 数组 
            string[] cnWY = new string[] { "", "[万]", "[亿]", "[万亿]" };
            string sRetun = ""; //返回值 
            int pos = 0; //字符位置指针 
            int mo = num.Length % 4; //取模 

            // 四位一组得到组数 
            int zuShu = (mo > 0 ? num.Length / 4 + 1 : num.Length / 4);

            // 外层循环在所有组中循环 
            // 从左到右 高位到低位 四位一组 逐组处理 
            // 每组最后加上一个单位: "[万亿]","[亿]","[万]" 
            for (int i = zuShu; i > 0; i--)
            {
                int weiShu = 4;//四位一组
                if (i == zuShu && mo != 0)//如果是最前面一组（最大的一组），并且模不等于0
                {
                    weiShu = mo;//最前面一组时，取模
                }
                // 得到一组四位数 最高位组有可能不足四位 
                string tempStrings = num.Substring(pos, weiShu);
                int sLength = tempStrings.Length;

                // 内层循环在该组中的每一位数上循环 从左到右 高位到低位 
                for (int j = 0; j < sLength; j++)
                {
                    //处理改组中的每一位数加上所在位: "仟","佰","拾",""(个) 
                    int n = Convert.ToInt32(tempStrings.Substring(j, 1));
                    if (n == 0)
                    {
                        if (j < sLength - 1 && Convert.ToInt32(tempStrings.Substring(j + 1, 1)) > 0 && !sRetun.EndsWith(cnNum[n]))//如果该0不是该组数字最后一位 并且 前一位大于0 并且 不是全部数字最后一位
                        {
                            sRetun += cnNum[n];
                        }
                    }
                    else
                    {
                        //处理 1013 一千零"十三", 1113 一千一百"一十三" 
                        if (!(n == 1 && (sRetun.EndsWith(cnNum[0]) | sRetun.Length == 0) && j == sLength - 2))//非（如果该数是1 且 是第一次运算 或者 返回数的长度为0） 且 该数是第二位
                        {
                            sRetun += cnNum[n];
                        }
                        sRetun += cnSBQ[sLength - j - 1];
                    }
                }
                pos += weiShu;
                // 每组最后加上一个单位: [万],[亿] 等 
                if (i < zuShu) //不是最高位的一组 
                {
                    if (Convert.ToInt32(tempStrings) != 0)
                    {
                        //如果所有 4 位不全是 0 则加上单位 [万],[亿] 等 
                        sRetun += cnWY[i - 1];
                    }
                }
                else
                {
                    //处理最高位的一组,最后必须加上单位 
                    sRetun += cnWY[i - 1];
                }
            }
            return sRetun;
        }

        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="cnNum">中文或其它语言的数组（如：one,two,three,four。。。）</param>
        /// <param name="cnSBQ">十百千数组（原理同上）</param>
        /// <param name="cnWY">万、亿数组（这样就支持任何语言了。例：萬、億）</param>
        /// <returns>返回格式化好的字符串</returns>
        internal static string ConvertNumberToChinese(string num, string[] cnNum, string[] cnSBQ, string[] cnWY)
        {
            string sRetun = ""; //返回值 
            int pos = 0; //字符位置指针 
            int mo = num.Length % 4; //取模 

            // 四位一组得到组数 
            int zuShu = (mo > 0 ? num.Length / 4 + 1 : num.Length / 4);

            // 外层循环在所有组中循环 
            // 从左到右 高位到低位 四位一组 逐组处理 
            // 每组最后加上一个单位: "[万亿]","[亿]","[万]" 
            for (int i = zuShu; i > 0; i--)
            {
                int weiShu = 4;//四位一组
                if (i == zuShu && mo != 0)//如果是最前面一组（最大的一组），并且模不等于0
                {
                    weiShu = mo;//最前面一组时，取模
                }
                // 得到一组四位数 最高位组有可能不足四位 
                string tempStrings = num.Substring(pos, weiShu);
                int sLength = tempStrings.Length;

                // 内层循环在该组中的每一位数上循环 从左到右 高位到低位 
                for (int j = 0; j < sLength; j++)
                {
                    //处理改组中的每一位数加上所在位: "仟","佰","拾",""(个) 
                    int n = Convert.ToInt32(tempStrings.Substring(j, 1));
                    if (n == 0)
                    {
                        if (j < sLength - 1 && Convert.ToInt32(tempStrings.Substring(j + 1, 1)) > 0 && !sRetun.EndsWith(cnNum[n]))//如果该0不是该组数字最后一位 并且 前一位大于0 并且 不是全部数字最后一位
                        {
                            sRetun += cnNum[n];
                        }
                    }
                    else
                    {
                        //处理 1013 一千零"十三", 1113 一千一百"一十三" 
                        if (!(n == 1 && (sRetun.EndsWith(cnNum[0]) | sRetun.Length == 0) && j == sLength - 2))//非（如果该数是1 且 是第一次运算 或者 返回数的长度为0） 且 该数是第二位
                        {
                            sRetun += cnNum[n];
                        }
                        sRetun += cnSBQ[sLength - j - 1];
                    }
                }
                pos += weiShu;
                // 每组最后加上一个单位: [万],[亿] 等 
                if (i < zuShu) //不是最高位的一组 
                {
                    if (Convert.ToInt32(tempStrings) != 0)
                    {
                        //如果所有 4 位不全是 0 则加上单位 [万],[亿] 等 
                        sRetun += cnWY[i - 1];
                    }
                }
                else
                {
                    //处理最高位的一组,最后必须加上单位 
                    sRetun += cnWY[i - 1];
                }
            }
            return sRetun;
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            var sBuilder = new StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取子查询条件，这需要处理多个模糊匹配的字符
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="search">模糊查询</param>
        /// <returns>表达式</returns>
        public static string GetLike(string field, string search)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < search.Length; i++)
            {
                returnValue += field + " LIKE '%" + search[i] + "%' AND ";
            }
            if (!string.IsNullOrEmpty(returnValue))
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 5);
            }
            returnValue = "(" + returnValue + ")";
            return returnValue;
        }

        /// <summary>
        /// 获取子查询条件，这里为精确查询
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="search">精确查询</param>
        /// <returns>表达式</returns>
        public static string GetEqual(string field, string search)
        {
            string returnValue = field + " = '" + search + "' ";
            returnValue = "(" + returnValue + ")";
            return returnValue;
        }

        #region public static string SqlSafe(string value) 检查参数的安全性
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="value">参数</param>
        /// <returns>安全的参数</returns>
        public static string SqlSafe(string value)
        {
            value = value.Replace("'", "''");
            // value = value.Replace("%", "'%");
            return value;
        }
        #endregion

        #region public static string GetSearchString(string searchValue, string allLike = null) 获取查询字符串
        /// <summary>
        /// 获取查询字符串
        /// </summary>
        /// <param name="searchValue">查询字符</param>
        /// <param name="allLike">是否所有的匹配都查询，建议传递"%"字符</param>
        /// <returns>字符串</returns>
        public static string GetSearchString(string searchValue, bool allLike = false)
        {
            searchValue = searchValue.Trim();
            searchValue = SqlSafe(searchValue);
            if (searchValue.Length > 0)
            {
                searchValue = searchValue.Replace('[', '_');
                searchValue = searchValue.Replace(']', '_');
            }
            if (searchValue == "%")
            {
                searchValue = "[%]";
            }
            if ((searchValue.Length > 0) && (searchValue.IndexOf('%') < 0) && (searchValue.IndexOf('_') < 0))
            {
                if (allLike)
                {
                    string searchLike = searchValue.Aggregate(string.Empty, (current, t) => current + ("%" + t));
                    searchValue = searchLike + "%";
                }
                else
                {
                    searchValue = "%" + searchValue + "%";
                }
            }
            return searchValue;
        }
        #endregion

        #region  public static bool Exists(string[] ids, string targetString) 判断是否包含的方法
        /// <summary>
        /// 判断是否包含的方法
        /// </summary>
        /// <param name="ids">数组</param>
        /// <param name="targetString">目标值</param>
        /// <returns>包含</returns>
        public static bool Exists(string[] ids, string targetString)
        {
            bool returnValue = false;
            if (ids != null && !string.IsNullOrEmpty(targetString))
            {
                if (ids.Any(t => t.Equals(targetString)))
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string[] Concat(string[] ids, string id)
        {
            return Concat(ids, new string[] { id });
        }

        #region 合并数组
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="ids">数组</param>
        /// <returns>数组</returns>
        public static string[] Concat(params string[][] ids)
        {
            // 进行合并
            Hashtable hashValues = new Hashtable();
            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] != null)
                    {
                        for (int j = 0; j < ids[i].Length; j++)
                        {
                            if (ids[i][j] != null)
                            {
                                if (!hashValues.ContainsKey(ids[i][j]))
                                {
                                    hashValues.Add(ids[i][j], ids[i][j]);
                                }
                            }
                        }
                    }
                }
            }
            // 返回合并结果
            string[] returnValues = new string[hashValues.Count];
            IDictionaryEnumerator enumerator = hashValues.GetEnumerator();
            int key = 0;
            while (enumerator.MoveNext())
            {
                returnValues[key] = (string)(enumerator.Key.ToString());
                key++;
            }
            return returnValues;
        }
        #endregion

        #region 从目标数组中去除某个值 public static string[] Remove(string[] ids, string id)
        /// <summary>
        /// 从目标数组中去除某个值
        /// </summary>
        /// <param name="ids">数组</param>
        /// <param name="id">目标值</param>
        /// <returns>数组</returns>
        public static string[] Remove(string[] ids, string id)
        {
            // 进行合并
            Hashtable hashValues = new Hashtable();
            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] != null && (!ids[i].Equals(id)))
                    {
                        if (!hashValues.ContainsKey(ids[i]))
                        {
                            hashValues.Add(ids[i], ids[i]);
                        }
                    }
                }
            }
            // 返回合并结果
            string[] returnValues = new string[hashValues.Count];
            IDictionaryEnumerator enumerator = hashValues.GetEnumerator();
            int key = 0;
            while (enumerator.MoveNext())
            {
                returnValues[key] = (string)(enumerator.Key.ToString());
                key++;
            }
            return returnValues;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string ArrayToList(string[] ids)
        {
            return ArrayToList(ids, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="separativeSign"></param>
        /// <returns></returns>
        public static string ArrayToList(string[] ids, string separativeSign)
        {
            int rowCount = 0;
            string returnValue = string.Empty;
            foreach (string id in ids)
            {
                rowCount++;
                returnValue += separativeSign + id + separativeSign + ",";
            }
            returnValue = rowCount == 0 ? "" : returnValue.TrimEnd(',');
            return returnValue;
        }

        #region public static string RepeatString(string targetString, int repeatCount) 重复字符串
        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="targetString">目标字符串</param>
        /// <param name="repeatCount">重复次数</param>
        /// <returns>结果字符串</returns>
        public static string RepeatString(string targetString, int repeatCount)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < repeatCount; i++)
            {
                returnValue += targetString;
            }
            return returnValue;
        }
        #endregion

        /// <summary> 
        /// 汉字转拼音缩写 
        /// 
        /// </summary> 
        /// <param name="strText">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string ToChineseSpell(string strText)
        {
            if (strText == null || strText.Trim().Length == 0)
            {
                return "";
            }

            // 汉字拼音首字母列表 本列表包含了20902个汉字
            StringBuilder sbChineseFirstPY = new StringBuilder();

            sbChineseFirstPY.Append(
                            "YDYQSXMWZSSXJBYMGCCZQPSSQBYCDSCDQLDYLYBSSJGYZZJJFKCCLZDHWDWZJLJPFYYNWJJTMYHZWZHFLZPPQHGSCYYYNJQYXXGJ");
            sbChineseFirstPY.Append(
                            "HHSDSJNKKTMOMLCRXYPSNQSECCQZGGLLYJLMYZZSECYKYYHQWJSSGGYXYZYJWWKDJHYCHMYXJTLXJYQBYXZLDWRDJRWYSRLDZJPC");
            sbChineseFirstPY.Append(
                            "BZJJBRCFTLECZSTZFXXZHTRQHYBDLYCZSSYMMRFMYQZPWWJJYFCRWFDFZQPYDDWYXKYJAWJFFXYPSFTZYHHYZYSWCJYXSCLCXXWZ");
            sbChineseFirstPY.Append(
                            "ZXNBGNNXBXLZSZSBSGPYSYZDHMDZBQBZCWDZZYYTZHBTSYYBZGNTNXQYWQSKBPHHLXGYBFMJEBJHHGQTJCYSXSTKZHLYCKGLYSMZ");
            sbChineseFirstPY.Append(
                            "XYALMELDCCXGZYRJXSDLTYZCQKCNNJWHJTZZCQLJSTSTBNXBTYXCEQXGKWJYFLZQLYHYXSPSFXLMPBYSXXXYDJCZYLLLSJXFHJXP");
            sbChineseFirstPY.Append(
                            "JBTFFYABYXBHZZBJYZLWLCZGGBTSSMDTJZXPTHYQTGLJSCQFZKJZJQNLZWLSLHDZBWJNCJZYZSQQYCQYRZCJJWYBRTWPYFTWEXCS");
            sbChineseFirstPY.Append(
                            "KDZCTBZHYZZYYJXZCFFZZMJYXXSDZZOTTBZLQWFCKSZSXFYRLNYJMBDTHJXSQQCCSBXYYTSYFBXDZTGBCNSLCYZZPSAZYZZSCJCS");
            sbChineseFirstPY.Append(
                            "HZQYDXLBPJLLMQXTYDZXSQJTZPXLCGLQTZWJBHCTSYJSFXYEJJTLBGXSXJMYJQQPFZASYJNTYDJXKJCDJSZCBARTDCLYJQMWNQNC");
            sbChineseFirstPY.Append(
                            "LLLKBYBZZSYHQQLTWLCCXTXLLZNTYLNEWYZYXCZXXGRKRMTCNDNJTSYYSSDQDGHSDBJGHRWRQLYBGLXHLGTGXBQJDZPYJSJYJCTM");
            sbChineseFirstPY.Append(
                            "RNYMGRZJCZGJMZMGXMPRYXKJNYMSGMZJYMKMFXMLDTGFBHCJHKYLPFMDXLQJJSMTQGZSJLQDLDGJYCALCMZCSDJLLNXDJFFFFJCZ");
            sbChineseFirstPY.Append(
                            "FMZFFPFKHKGDPSXKTACJDHHZDDCRRCFQYJKQCCWJDXHWJLYLLZGCFCQDSMLZPBJJPLSBCJGGDCKKDEZSQCCKJGCGKDJTJDLZYCXK");
            sbChineseFirstPY.Append(
                            "LQSCGJCLTFPCQCZGWPJDQYZJJBYJHSJDZWGFSJGZKQCCZLLPSPKJGQJHZZLJPLGJGJJTHJJYJZCZMLZLYQBGJWMLJKXZDZNJQSYZ");
            sbChineseFirstPY.Append(
                            "MLJLLJKYWXMKJLHSKJGBMCLYYMKXJQLBMLLKMDXXKWYXYSLMLPSJQQJQXYXFJTJDXMXXLLCXQBSYJBGWYMBGGBCYXPJYGPEPFGDJ");
            sbChineseFirstPY.Append(
                            "GBHBNSQJYZJKJKHXQFGQZKFHYGKHDKLLSDJQXPQYKYBNQSXQNSZSWHBSXWHXWBZZXDMNSJBSBKBBZKLYLXGWXDRWYQZMYWSJQLCJ");
            sbChineseFirstPY.Append(
                            "XXJXKJEQXSCYETLZHLYYYSDZPAQYZCMTLSHTZCFYZYXYLJSDCJQAGYSLCQLYYYSHMRQQKLDXZSCSSSYDYCJYSFSJBFRSSZQSBXXP");
            sbChineseFirstPY.Append(
                            "XJYSDRCKGJLGDKZJZBDKTCSYQPYHSTCLDJDHMXMCGXYZHJDDTMHLTXZXYLYMOHYJCLTYFBQQXPFBDFHHTKSQHZYYWCNXXCRWHOWG");
            sbChineseFirstPY.Append(
                            "YJLEGWDQCWGFJYCSNTMYTOLBYGWQWESJPWNMLRYDZSZTXYQPZGCWXHNGPYXSHMYQJXZTDPPBFYHZHTJYFDZWKGKZBLDNTSXHQEEG");
            sbChineseFirstPY.Append(
                            "ZZYLZMMZYJZGXZXKHKSTXNXXWYLYAPSTHXDWHZYMPXAGKYDXBHNHXKDPJNMYHYLPMGOCSLNZHKXXLPZZLBMLSFBHHGYGYYGGBHSC");
            sbChineseFirstPY.Append(
                            "YAQTYWLXTZQCEZYDQDQMMHTKLLSZHLSJZWFYHQSWSCWLQAZYNYTLSXTHAZNKZZSZZLAXXZWWCTGQQTDDYZTCCHYQZFLXPSLZYGPZ");
            sbChineseFirstPY.Append(
                            "SZNGLNDQTBDLXGTCTAJDKYWNSYZLJHHZZCWNYYZYWMHYCHHYXHJKZWSXHZYXLYSKQYSPSLYZWMYPPKBYGLKZHTYXAXQSYSHXASMC");
            sbChineseFirstPY.Append(
                            "HKDSCRSWJPWXSGZJLWWSCHSJHSQNHCSEGNDAQTBAALZZMSSTDQJCJKTSCJAXPLGGXHHGXXZCXPDMMHLDGTYBYSJMXHMRCPXXJZCK");
            sbChineseFirstPY.Append(
                            "ZXSHMLQXXTTHXWZFKHCCZDYTCJYXQHLXDHYPJQXYLSYYDZOZJNYXQEZYSQYAYXWYPDGXDDXSPPYZNDLTWRHXYDXZZJHTCXMCZLHP");
            sbChineseFirstPY.Append(
                            "YYYYMHZLLHNXMYLLLMDCPPXHMXDKYCYRDLTXJCHHZZXZLCCLYLNZSHZJZZLNNRLWHYQSNJHXYNTTTKYJPYCHHYEGKCTTWLGQRLGG");
            sbChineseFirstPY.Append(
                            "TGTYGYHPYHYLQYQGCWYQKPYYYTTTTLHYHLLTYTTSPLKYZXGZWGPYDSSZZDQXSKCQNMJJZZBXYQMJRTFFBTKHZKBXLJJKDXJTLBWF");
            sbChineseFirstPY.Append(
                            "ZPPTKQTZTGPDGNTPJYFALQMKGXBDCLZFHZCLLLLADPMXDJHLCCLGYHDZFGYDDGCYYFGYDXKSSEBDHYKDKDKHNAXXYBPBYYHXZQGA");
            sbChineseFirstPY.Append(
                            "FFQYJXDMLJCSQZLLPCHBSXGJYNDYBYQSPZWJLZKSDDTACTBXZDYZYPJZQSJNKKTKNJDJGYYPGTLFYQKASDNTCYHBLWDZHBBYDWJR");
            sbChineseFirstPY.Append(
                            "YGKZYHEYYFJMSDTYFZJJHGCXPLXHLDWXXJKYTCYKSSSMTWCTTQZLPBSZDZWZXGZAGYKTYWXLHLSPBCLLOQMMZSSLCMBJCSZZKYDC");
            sbChineseFirstPY.Append(
                            "ZJGQQDSMCYTZQQLWZQZXSSFPTTFQMDDZDSHDTDWFHTDYZJYQJQKYPBDJYYXTLJHDRQXXXHAYDHRJLKLYTWHLLRLLRCXYLBWSRSZZ");
            sbChineseFirstPY.Append(
                            "SYMKZZHHKYHXKSMDSYDYCJPBZBSQLFCXXXNXKXWYWSDZYQOGGQMMYHCDZTTFJYYBGSTTTYBYKJDHKYXBELHTYPJQNFXFDYKZHQKZ");
            sbChineseFirstPY.Append(
                            "BYJTZBXHFDXKDASWTAWAJLDYJSFHBLDNNTNQJTJNCHXFJSRFWHZFMDRYJYJWZPDJKZYJYMPCYZNYNXFBYTFYFWYGDBNZZZDNYTXZ");
            sbChineseFirstPY.Append(
                            "EMMQBSQEHXFZMBMFLZZSRXYMJGSXWZJSPRYDJSJGXHJJGLJJYNZZJXHGXKYMLPYYYCXYTWQZSWHWLYRJLPXSLSXMFSWWKLCTNXNY");
            sbChineseFirstPY.Append(
                            "NPSJSZHDZEPTXMYYWXYYSYWLXJQZQXZDCLEEELMCPJPCLWBXSQHFWWTFFJTNQJHJQDXHWLBYZNFJLALKYYJLDXHHYCSTYYWNRJYX");
            sbChineseFirstPY.Append(
                            "YWTRMDRQHWQCMFJDYZMHMYYXJWMYZQZXTLMRSPWWCHAQBXYGZYPXYYRRCLMPYMGKSJSZYSRMYJSNXTPLNBAPPYPYLXYYZKYNLDZY");
            sbChineseFirstPY.Append(
                            "JZCZNNLMZHHARQMPGWQTZMXXMLLHGDZXYHXKYXYCJMFFYYHJFSBSSQLXXNDYCANNMTCJCYPRRNYTYQNYYMBMSXNDLYLYSLJRLXYS");
            sbChineseFirstPY.Append(
                            "XQMLLYZLZJJJKYZZCSFBZXXMSTBJGNXYZHLXNMCWSCYZYFZLXBRNNNYLBNRTGZQYSATSWRYHYJZMZDHZGZDWYBSSCSKXSYHYTXXG");
            sbChineseFirstPY.Append(
                            "CQGXZZSHYXJSCRHMKKBXCZJYJYMKQHZJFNBHMQHYSNJNZYBKNQMCLGQHWLZNZSWXKHLJHYYBQLBFCDSXDLDSPFZPSKJYZWZXZDDX");
            sbChineseFirstPY.Append(
                            "JSMMEGJSCSSMGCLXXKYYYLNYPWWWGYDKZJGGGZGGSYCKNJWNJPCXBJJTQTJWDSSPJXZXNZXUMELPXFSXTLLXCLJXJJLJZXCTPSWX");
            sbChineseFirstPY.Append(
                            "LYDHLYQRWHSYCSQYYBYAYWJJJQFWQCQQCJQGXALDBZZYJGKGXPLTZYFXJLTPADKYQHPMATLCPDCKBMTXYBHKLENXDLEEGQDYMSAW");
            sbChineseFirstPY.Append(
                            "HZMLJTWYGXLYQZLJEEYYBQQFFNLYXRDSCTGJGXYYNKLLYQKCCTLHJLQMKKZGCYYGLLLJDZGYDHZWXPYSJBZKDZGYZZHYWYFQYTYZ");
            sbChineseFirstPY.Append(
                            "SZYEZZLYMHJJHTSMQWYZLKYYWZCSRKQYTLTDXWCTYJKLWSQZWBDCQYNCJSRSZJLKCDCDTLZZZACQQZZDDXYPLXZBQJYLZLLLQDDZ");
            sbChineseFirstPY.Append(
                            "QJYJYJZYXNYYYNYJXKXDAZWYRDLJYYYRJLXLLDYXJCYWYWNQCCLDDNYYYNYCKCZHXXCCLGZQJGKWPPCQQJYSBZZXYJSQPXJPZBSB");
            sbChineseFirstPY.Append(
                            "DSFNSFPZXHDWZTDWPPTFLZZBZDMYYPQJRSDZSQZSQXBDGCPZSWDWCSQZGMDHZXMWWFYBPDGPHTMJTHZSMMBGZMBZJCFZWFZBBZMQ");
            sbChineseFirstPY.Append(
                            "CFMBDMCJXLGPNJBBXGYHYYJGPTZGZMQBQTCGYXJXLWZKYDPDYMGCFTPFXYZTZXDZXTGKMTYBBCLBJASKYTSSQYYMSZXFJEWLXLLS");
            sbChineseFirstPY.Append(
                            "ZBQJJJAKLYLXLYCCTSXMCWFKKKBSXLLLLJYXTYLTJYYTDPJHNHNNKBYQNFQYYZBYYESSESSGDYHFHWTCJBSDZZTFDMXHCNJZYMQW");
            sbChineseFirstPY.Append(
                            "SRYJDZJQPDQBBSTJGGFBKJBXTGQHNGWJXJGDLLTHZHHYYYYYYSXWTYYYCCBDBPYPZYCCZYJPZYWCBDLFWZCWJDXXHYHLHWZZXJTC");
            sbChineseFirstPY.Append(
                            "ZLCDPXUJCZZZLYXJJTXPHFXWPYWXZPTDZZBDZCYHJHMLXBQXSBYLRDTGJRRCTTTHYTCZWMXFYTWWZCWJWXJYWCSKYBZSCCTZQNHX");
            sbChineseFirstPY.Append(
                            "NWXXKHKFHTSWOCCJYBCMPZZYKBNNZPBZHHZDLSYDDYTYFJPXYNGFXBYQXCBHXCPSXTYZDMKYSNXSXLHKMZXLYHDHKWHXXSSKQYHH");
            sbChineseFirstPY.Append(
                            "CJYXGLHZXCSNHEKDTGZXQYPKDHEXTYKCNYMYYYPKQYYYKXZLTHJQTBYQHXBMYHSQCKWWYLLHCYYLNNEQXQWMCFBDCCMLJGGXDQKT");
            sbChineseFirstPY.Append(
                            "LXKGNQCDGZJWYJJLYHHQTTTNWCHMXCXWHWSZJYDJCCDBQCDGDNYXZTHCQRXCBHZTQCBXWGQWYYBXHMBYMYQTYEXMQKYAQYRGYZSL");
            sbChineseFirstPY.Append(
                            "FYKKQHYSSQYSHJGJCNXKZYCXSBXYXHYYLSTYCXQTHYSMGSCPMMGCCCCCMTZTASMGQZJHKLOSQYLSWTMXSYQKDZLJQQYPLSYCZTCQ");
            sbChineseFirstPY.Append(
                            "QPBBQJZCLPKHQZYYXXDTDDTSJCXFFLLCHQXMJLWCJCXTSPYCXNDTJSHJWXDQQJSKXYAMYLSJHMLALYKXCYYDMNMDQMXMCZNNCYBZ");
            sbChineseFirstPY.Append(
                            "KKYFLMCHCMLHXRCJJHSYLNMTJZGZGYWJXSRXCWJGJQHQZDQJDCJJZKJKGDZQGJJYJYLXZXXCDQHHHEYTMHLFSBDJSYYSHFYSTCZQ");
            sbChineseFirstPY.Append(
                            "LPBDRFRZTZYKYWHSZYQKWDQZRKMSYNBCRXQBJYFAZPZZEDZCJYWBCJWHYJBQSZYWRYSZPTDKZPFPBNZTKLQYHBBZPNPPTYZZYBQN");
            sbChineseFirstPY.Append(
                            "YDCPJMMCYCQMCYFZZDCMNLFPBPLNGQJTBTTNJZPZBBZNJKLJQYLNBZQHKSJZNGGQSZZKYXSHPZSNBCGZKDDZQANZHJKDRTLZLSWJ");
            sbChineseFirstPY.Append(
                            "LJZLYWTJNDJZJHXYAYNCBGTZCSSQMNJPJYTYSWXZFKWJQTKHTZPLBHSNJZSYZBWZZZZLSYLSBJHDWWQPSLMMFBJDWAQYZTCJTBNN");
            sbChineseFirstPY.Append(
                            "WZXQXCDSLQGDSDPDZHJTQQPSWLYYJZLGYXYZLCTCBJTKTYCZJTQKBSJLGMGZDMCSGPYNJZYQYYKNXRPWSZXMTNCSZZYXYBYHYZAX");
            sbChineseFirstPY.Append(
                            "YWQCJTLLCKJJTJHGDXDXYQYZZBYWDLWQCGLZGJGQRQZCZSSBCRPCSKYDZNXJSQGXSSJMYDNSTZTPBDLTKZWXQWQTZEXNQCZGWEZK");
            sbChineseFirstPY.Append(
                            "SSBYBRTSSSLCCGBPSZQSZLCCGLLLZXHZQTHCZMQGYZQZNMCOCSZJMMZSQPJYGQLJYJPPLDXRGZYXCCSXHSHGTZNLZWZKJCXTCFCJ");
            sbChineseFirstPY.Append(
                            "XLBMQBCZZWPQDNHXLJCTHYZLGYLNLSZZPCXDSCQQHJQKSXZPBAJYEMSMJTZDXLCJYRYYNWJBNGZZTMJXLTBSLYRZPYLSSCNXPHLL");
            sbChineseFirstPY.Append(
                            "HYLLQQZQLXYMRSYCXZLMMCZLTZSDWTJJLLNZGGQXPFSKYGYGHBFZPDKMWGHCXMSGDXJMCJZDYCABXJDLNBCDQYGSKYDQTXDJJYXM");
            sbChineseFirstPY.Append(
                            "SZQAZDZFSLQXYJSJZYLBTXXWXQQZBJZUFBBLYLWDSLJHXJYZJWTDJCZFQZQZZDZSXZZQLZCDZFJHYSPYMPQZMLPPLFFXJJNZZYLS");
            sbChineseFirstPY.Append(
                            "JEYQZFPFZKSYWJJJHRDJZZXTXXGLGHYDXCSKYSWMMZCWYBAZBJKSHFHJCXMHFQHYXXYZFTSJYZFXYXPZLCHMZMBXHZZSXYFYMNCW");
            sbChineseFirstPY.Append(
                            "DABAZLXKTCSHHXKXJJZJSTHYGXSXYYHHHJWXKZXSSBZZWHHHCWTZZZPJXSNXQQJGZYZYWLLCWXZFXXYXYHXMKYYSWSQMNLNAYCYS");
            sbChineseFirstPY.Append(
                            "PMJKHWCQHYLAJJMZXHMMCNZHBHXCLXTJPLTXYJHDYYLTTXFSZHYXXSJBJYAYRSMXYPLCKDUYHLXRLNLLSTYZYYQYGYHHSCCSMZCT");
            sbChineseFirstPY.Append(
                            "ZQXKYQFPYYRPFFLKQUNTSZLLZMWWTCQQYZWTLLMLMPWMBZSSTZRBPDDTLQJJBXZCSRZQQYGWCSXFWZLXCCRSZDZMCYGGDZQSGTJS");
            sbChineseFirstPY.Append(
                            "WLJMYMMZYHFBJDGYXCCPSHXNZCSBSJYJGJMPPWAFFYFNXHYZXZYLREMZGZCYZSSZDLLJCSQFNXZKPTXZGXJJGFMYYYSNBTYLBNLH");
            sbChineseFirstPY.Append(
                            "PFZDCYFBMGQRRSSSZXYSGTZRNYDZZCDGPJAFJFZKNZBLCZSZPSGCYCJSZLMLRSZBZZLDLSLLYSXSQZQLYXZLSKKBRXBRBZCYCXZZ");
            sbChineseFirstPY.Append(
                            "ZEEYFGKLZLYYHGZSGZLFJHGTGWKRAAJYZKZQTSSHJJXDCYZUYJLZYRZDQQHGJZXSSZBYKJPBFRTJXLLFQWJHYLQTYMBLPZDXTZYG");
            sbChineseFirstPY.Append(
                            "BDHZZRBGXHWNJTJXLKSCFSMWLSDQYSJTXKZSCFWJLBXFTZLLJZLLQBLSQMQQCGCZFPBPHZCZJLPYYGGDTGWDCFCZQYYYQYSSCLXZ");
            sbChineseFirstPY.Append(
                            "SKLZZZGFFCQNWGLHQYZJJCZLQZZYJPJZZBPDCCMHJGXDQDGDLZQMFGPSYTSDYFWWDJZJYSXYYCZCYHZWPBYKXRYLYBHKJKSFXTZJ");
            sbChineseFirstPY.Append(
                            "MMCKHLLTNYYMSYXYZPYJQYCSYCWMTJJKQYRHLLQXPSGTLYYCLJSCPXJYZFNMLRGJJTYZBXYZMSJYJHHFZQMSYXRSZCWTLRTQZSST");
            sbChineseFirstPY.Append(
                            "KXGQKGSPTGCZNJSJCQCXHMXGGZTQYDJKZDLBZSXJLHYQGGGTHQSZPYHJHHGYYGKGGCWJZZYLCZLXQSFTGZSLLLMLJSKCTBLLZZSZ");
            sbChineseFirstPY.Append(
                            "MMNYTPZSXQHJCJYQXYZXZQZCPSHKZZYSXCDFGMWQRLLQXRFZTLYSTCTMJCXJJXHJNXTNRZTZFQYHQGLLGCXSZSJDJLJCYDSJTLNY");
            sbChineseFirstPY.Append(
                            "XHSZXCGJZYQPYLFHDJSBPCCZHJJJQZJQDYBSSLLCMYTTMQTBHJQNNYGKYRQYQMZGCJKPDCGMYZHQLLSLLCLMHOLZGDYYFZSLJCQZ");
            sbChineseFirstPY.Append(
                            "LYLZQJESHNYLLJXGJXLYSYYYXNBZLJSSZCQQCJYLLZLTJYLLZLLBNYLGQCHXYYXOXCXQKYJXXXYKLXSXXYQXCYKQXQCSGYXXYQXY");
            sbChineseFirstPY.Append(
                            "GYTQOHXHXPYXXXULCYEYCHZZCBWQBBWJQZSCSZSSLZYLKDESJZWMYMCYTSDSXXSCJPQQSQYLYYZYCMDJDZYWCBTJSYDJKCYDDJLB");
            sbChineseFirstPY.Append(
                            "DJJSODZYSYXQQYXDHHGQQYQHDYXWGMMMAJDYBBBPPBCMUUPLJZSMTXERXJMHQNUTPJDCBSSMSSSTKJTSSMMTRCPLZSZMLQDSDMJM");
            sbChineseFirstPY.Append(
                            "QPNQDXCFYNBFSDQXYXHYAYKQYDDLQYYYSSZBYDSLNTFQTZQPZMCHDHCZCWFDXTMYQSPHQYYXSRGJCWTJTZZQMGWJJTJHTQJBBHWZ");
            sbChineseFirstPY.Append(
                            "PXXHYQFXXQYWYYHYSCDYDHHQMNMTMWCPBSZPPZZGLMZFOLLCFWHMMSJZTTDHZZYFFYTZZGZYSKYJXQYJZQBHMBZZLYGHGFMSHPZF");
            sbChineseFirstPY.Append(
                            "ZSNCLPBQSNJXZSLXXFPMTYJYGBXLLDLXPZJYZJYHHZCYWHJYLSJEXFSZZYWXKZJLUYDTMLYMQJPWXYHXSKTQJEZRPXXZHHMHWQPW");
            sbChineseFirstPY.Append(
                            "QLYJJQJJZSZCPHJLCHHNXJLQWZJHBMZYXBDHHYPZLHLHLGFWLCHYYTLHJXCJMSCPXSTKPNHQXSRTYXXTESYJCTLSSLSTDLLLWWYH");
            sbChineseFirstPY.Append(
                            "DHRJZSFGXTSYCZYNYHTDHWJSLHTZDQDJZXXQHGYLTZPHCSQFCLNJTCLZPFSTPDYNYLGMJLLYCQHYSSHCHYLHQYQTMZYPBYWRFQYK");
            sbChineseFirstPY.Append(
                            "QSYSLZDQJMPXYYSSRHZJNYWTQDFZBWWTWWRXCWHGYHXMKMYYYQMSMZHNGCEPMLQQMTCWCTMMPXJPJJHFXYYZSXZHTYBMSTSYJTTQ");
            sbChineseFirstPY.Append(
                            "QQYYLHYNPYQZLCYZHZWSMYLKFJXLWGXYPJYTYSYXYMZCKTTWLKSMZSYLMPWLZWXWQZSSAQSYXYRHSSNTSRAPXCPWCMGDXHXZDZYF");
            sbChineseFirstPY.Append(
                            "JHGZTTSBJHGYZSZYSMYCLLLXBTYXHBBZJKSSDMALXHYCFYGMQYPJYCQXJLLLJGSLZGQLYCJCCZOTYXMTMTTLLWTGPXYMZMKLPSZZ");
            sbChineseFirstPY.Append(
                            "ZXHKQYSXCTYJZYHXSHYXZKXLZWPSQPYHJWPJPWXQQYLXSDHMRSLZZYZWTTCYXYSZZSHBSCCSTPLWSSCJCHNLCGCHSSPHYLHFHHXJ");
            sbChineseFirstPY.Append(
                            "SXYLLNYLSZDHZXYLSXLWZYKCLDYAXZCMDDYSPJTQJZLNWQPSSSWCTSTSZLBLNXSMNYYMJQBQHRZWTYYDCHQLXKPZWBGQYBKFCMZW");
            sbChineseFirstPY.Append(
                            "PZLLYYLSZYDWHXPSBCMLJBSCGBHXLQHYRLJXYSWXWXZSLDFHLSLYNJLZYFLYJYCDRJLFSYZFSLLCQYQFGJYHYXZLYLMSTDJCYHBZ");
            sbChineseFirstPY.Append(
                            "LLNWLXXYGYYHSMGDHXXHHLZZJZXCZZZCYQZFNGWPYLCPKPYYPMCLQKDGXZGGWQBDXZZKZFBXXLZXJTPJPTTBYTSZZDWSLCHZHSLT");
            sbChineseFirstPY.Append(
                            "YXHQLHYXXXYYZYSWTXZKHLXZXZPYHGCHKCFSYHUTJRLXFJXPTZTWHPLYXFCRHXSHXKYXXYHZQDXQWULHYHMJTBFLKHTXCWHJFWJC");
            sbChineseFirstPY.Append(
                            "FPQRYQXCYYYQYGRPYWSGSUNGWCHKZDXYFLXXHJJBYZWTSXXNCYJJYMSWZJQRMHXZWFQSYLZJZGBHYNSLBGTTCSYBYXXWXYHXYYXN");
            sbChineseFirstPY.Append(
                            "SQYXMQYWRGYQLXBBZLJSYLPSYTJZYHYZAWLRORJMKSCZJXXXYXCHDYXRYXXJDTSQFXLYLTSFFYXLMTYJMJUYYYXLTZCSXQZQHZXL");
            sbChineseFirstPY.Append(
                            "YYXZHDNBRXXXJCTYHLBRLMBRLLAXKYLLLJLYXXLYCRYLCJTGJCMTLZLLCYZZPZPCYAWHJJFYBDYYZSMPCKZDQYQPBPCJPDCYZMDP");
            sbChineseFirstPY.Append(
                            "BCYYDYCNNPLMTMLRMFMMGWYZBSJGYGSMZQQQZTXMKQWGXLLPJGZBQCDJJJFPKJKCXBLJMSWMDTQJXLDLPPBXCWRCQFBFQJCZAHZG");
            sbChineseFirstPY.Append(
                            "MYKPHYYHZYKNDKZMBPJYXPXYHLFPNYYGXJDBKXNXHJMZJXSTRSTLDXSKZYSYBZXJLXYSLBZYSLHXJPFXPQNBYLLJQKYGZMCYZZYM");
            sbChineseFirstPY.Append(
                            "CCSLCLHZFWFWYXZMWSXTYNXJHPYYMCYSPMHYSMYDYSHQYZCHMJJMZCAAGCFJBBHPLYZYLXXSDJGXDHKXXTXXNBHRMLYJSLTXMRHN");
            sbChineseFirstPY.Append(
                            "LXQJXYZLLYSWQGDLBJHDCGJYQYCMHWFMJYBMBYJYJWYMDPWHXQLDYGPDFXXBCGJSPCKRSSYZJMSLBZZJFLJJJLGXZGYXYXLSZQYX");
            sbChineseFirstPY.Append(
                            "BEXYXHGCXBPLDYHWETTWWCJMBTXCHXYQXLLXFLYXLLJLSSFWDPZSMYJCLMWYTCZPCHQEKCQBWLCQYDPLQPPQZQFJQDJHYMMCXTXD");
            sbChineseFirstPY.Append(
                            "RMJWRHXCJZYLQXDYYNHYYHRSLSRSYWWZJYMTLTLLGTQCJZYABTCKZCJYCCQLJZQXALMZYHYWLWDXZXQDLLQSHGPJFJLJHJABCQZD");
            sbChineseFirstPY.Append(
                            "JGTKHSSTCYJLPSWZLXZXRWGLDLZRLZXTGSLLLLZLYXXWGDZYGBDPHZPBRLWSXQBPFDWOFMWHLYPCBJCCLDMBZPBZZLCYQXLDOMZB");
            sbChineseFirstPY.Append(
                            "LZWPDWYYGDSTTHCSQSCCRSSSYSLFYBFNTYJSZDFNDPDHDZZMBBLSLCMYFFGTJJQWFTMTPJWFNLBZCMMJTGBDZLQLPYFHYYMJYLSD");
            sbChineseFirstPY.Append(
                            "CHDZJWJCCTLJCLDTLJJCPDDSQDSSZYBNDBJLGGJZXSXNLYCYBJXQYCBYLZCFZPPGKCXZDZFZTJJFJSJXZBNZYJQTTYJYHTYCZHYM");
            sbChineseFirstPY.Append(
                            "DJXTTMPXSPLZCDWSLSHXYPZGTFMLCJTYCBPMGDKWYCYZCDSZZYHFLYCTYGWHKJYYLSJCXGYWJCBLLCSNDDBTZBSCLYZCZZSSQDLL");
            sbChineseFirstPY.Append(
                            "MQYYHFSLQLLXFTYHABXGWNYWYYPLLSDLDLLBJCYXJZMLHLJDXYYQYTDLLLBUGBFDFBBQJZZMDPJHGCLGMJJPGAEHHBWCQXAXHHHZ");
            sbChineseFirstPY.Append(
                            "CHXYPHJAXHLPHJPGPZJQCQZGJJZZUZDMQYYBZZPHYHYBWHAZYJHYKFGDPFQSDLZMLJXKXGALXZDAGLMDGXMWZQYXXDXXPFDMMSSY");
            sbChineseFirstPY.Append(
                            "MPFMDMMKXKSYZYSHDZKXSYSMMZZZMSYDNZZCZXFPLSTMZDNMXCKJMZTYYMZMZZMSXHHDCZJEMXXKLJSTLWLSQLYJZLLZJSSDPPMH");
            sbChineseFirstPY.Append(
                            "NLZJCZYHMXXHGZCJMDHXTKGRMXFWMCGMWKDTKSXQMMMFZZYDKMSCLCMPCGMHSPXQPZDSSLCXKYXTWLWJYAHZJGZQMCSNXYYMMPML");
            sbChineseFirstPY.Append(
                            "KJXMHLMLQMXCTKZMJQYSZJSYSZHSYJZJCDAJZYBSDQJZGWZQQXFKDMSDJLFWEHKZQKJPEYPZYSZCDWYJFFMZZYLTTDZZEFMZLBNP");
            sbChineseFirstPY.Append(
                            "PLPLPEPSZALLTYLKCKQZKGENQLWAGYXYDPXLHSXQQWQCQXQCLHYXXMLYCCWLYMQYSKGCHLCJNSZKPYZKCQZQLJPDMDZHLASXLBYD");
            sbChineseFirstPY.Append(
                            "WQLWDNBQCRYDDZTJYBKBWSZDXDTNPJDTCTQDFXQQMGNXECLTTBKPWSLCTYQLPWYZZKLPYGZCQQPLLKCCYLPQMZCZQCLJSLQZDJXL");
            sbChineseFirstPY.Append(
                            "DDHPZQDLJJXZQDXYZQKZLJCYQDYJPPYPQYKJYRMPCBYMCXKLLZLLFQPYLLLMBSGLCYSSLRSYSQTMXYXZQZFDZUYSYZTFFMZZSMZQ");
            sbChineseFirstPY.Append(
                            "HZSSCCMLYXWTPZGXZJGZGSJSGKDDHTQGGZLLBJDZLCBCHYXYZHZFYWXYZYMSDBZZYJGTSMTFXQYXQSTDGSLNXDLRYZZLRYYLXQHT");
            sbChineseFirstPY.Append(
                            "XSRTZNGZXBNQQZFMYKMZJBZYMKBPNLYZPBLMCNQYZZZSJZHJCTZKHYZZJRDYZHNPXGLFZTLKGJTCTSSYLLGZRZBBQZZKLPKLCZYS");
            sbChineseFirstPY.Append(
                            "SUYXBJFPNJZZXCDWXZYJXZZDJJKGGRSRJKMSMZJLSJYWQSKYHQJSXPJZZZLSNSHRNYPZTWCHKLPSRZLZXYJQXQKYSJYCZTLQZYBB");
            sbChineseFirstPY.Append(
                            "YBWZPQDWWYZCYTJCJXCKCWDKKZXSGKDZXWWYYJQYYTCYTDLLXWKCZKKLCCLZCQQDZLQLCSFQCHQHSFSMQZZLNBJJZBSJHTSZDYSJ");
            sbChineseFirstPY.Append(
                            "QJPDLZCDCWJKJZZLPYCGMZWDJJBSJQZSYZYHHXJPBJYDSSXDZNCGLQMBTSFSBPDZDLZNFGFJGFSMPXJQLMBLGQCYYXBQKDJJQYRF");
            sbChineseFirstPY.Append(
                            "KZTJDHCZKLBSDZCFJTPLLJGXHYXZCSSZZXSTJYGKGCKGYOQXJPLZPBPGTGYJZGHZQZZLBJLSQFZGKQQJZGYCZBZQTLDXRJXBSXXP");
            sbChineseFirstPY.Append(
                            "ZXHYZYCLWDXJJHXMFDZPFZHQHQMQGKSLYHTYCGFRZGNQXCLPDLBZCSCZQLLJBLHBZCYPZZPPDYMZZSGYHCKCPZJGSLJLNSCDSLDL");
            sbChineseFirstPY.Append(
                            "XBMSTLDDFJMKDJDHZLZXLSZQPQPGJLLYBDSZGQLBZLSLKYYHZTTNTJYQTZZPSZQZTLLJTYYLLQLLQYZQLBDZLSLYYZYMDFSZSNHL");
            sbChineseFirstPY.Append(
                            "XZNCZQZPBWSKRFBSYZMTHBLGJPMCZZLSTLXSHTCSYZLZBLFEQHLXFLCJLYLJQCBZLZJHHSSTBRMHXZHJZCLXFNBGXGTQJCZTMSFZ");
            sbChineseFirstPY.Append(
                            "KJMSSNXLJKBHSJXNTNLZDNTLMSJXGZJYJCZXYJYJWRWWQNZTNFJSZPZSHZJFYRDJSFSZJZBJFZQZZHZLXFYSBZQLZSGYFTZDCSZX");
            sbChineseFirstPY.Append(
                            "ZJBQMSZKJRHYJZCKMJKHCHGTXKXQGLXPXFXTRTYLXJXHDTSJXHJZJXZWZLCQSBTXWXGXTXXHXFTSDKFJHZYJFJXRZSDLLLTQSQQZ");
            sbChineseFirstPY.Append(
                            "QWZXSYQTWGWBZCGZLLYZBCLMQQTZHZXZXLJFRMYZFLXYSQXXJKXRMQDZDMMYYBSQBHGZMWFWXGMXLZPYYTGZYCCDXYZXYWGSYJYZ");
            sbChineseFirstPY.Append(
                            "NBHPZJSQSYXSXRTFYZGRHZTXSZZTHCBFCLSYXZLZQMZLMPLMXZJXSFLBYZMYQHXJSXRXSQZZZSSLYFRCZJRCRXHHZXQYDYHXSJJH");
            sbChineseFirstPY.Append(
                            "ZCXZBTYNSYSXJBQLPXZQPYMLXZKYXLXCJLCYSXXZZLXDLLLJJYHZXGYJWKJRWYHCPSGNRZLFZWFZZNSXGXFLZSXZZZBFCSYJDBRJ");
            sbChineseFirstPY.Append(
                            "KRDHHGXJLJJTGXJXXSTJTJXLYXQFCSGSWMSBCTLQZZWLZZKXJMLTMJYHSDDBXGZHDLBMYJFRZFSGCLYJBPMLYSMSXLSZJQQHJZFX");
            sbChineseFirstPY.Append(
                            "GFQFQBPXZGYYQXGZTCQWYLTLGWSGWHRLFSFGZJMGMGBGTJFSYZZGZYZAFLSSPMLPFLCWBJZCLJJMZLPJJLYMQDMYYYFBGYGYZMLY");
            sbChineseFirstPY.Append(
                            "ZDXQYXRQQQHSYYYQXYLJTYXFSFSLLGNQCYHYCWFHCCCFXPYLYPLLZYXXXXXKQHHXSHJZCFZSCZJXCPZWHHHHHAPYLQALPQAFYHXD");
            sbChineseFirstPY.Append(
                            "YLUKMZQGGGDDESRNNZLTZGCHYPPYSQJJHCLLJTOLNJPZLJLHYMHEYDYDSQYCDDHGZUNDZCLZYZLLZNTNYZGSLHSLPJJBDGWXPCDU");
            sbChineseFirstPY.Append(
                            "TJCKLKCLWKLLCASSTKZZDNQNTTLYYZSSYSSZZRYLJQKCQDHHCRXRZYDGRGCWCGZQFFFPPJFZYNAKRGYWYQPQXXFKJTSZZXSWZDDF");
            sbChineseFirstPY.Append(
                            "BBXTBGTZKZNPZZPZXZPJSZBMQHKCYXYLDKLJNYPKYGHGDZJXXEAHPNZKZTZCMXCXMMJXNKSZQNMNLWBWWXJKYHCPSTMCSQTZJYXT");
            sbChineseFirstPY.Append(
                            "PCTPDTNNPGLLLZSJLSPBLPLQHDTNJNLYYRSZFFJFQWDPHZDWMRZCCLODAXNSSNYZRESTYJWJYJDBCFXNMWTTBYLWSTSZGYBLJPXG");
            sbChineseFirstPY.Append(
                            "LBOCLHPCBJLTMXZLJYLZXCLTPNCLCKXTPZJSWCYXSFYSZDKNTLBYJCYJLLSTGQCBXRYZXBXKLYLHZLQZLNZCXWJZLJZJNCJHXMNZ");
            sbChineseFirstPY.Append(
                            "ZGJZZXTZJXYCYYCXXJYYXJJXSSSJSTSSTTPPGQTCSXWZDCSYFPTFBFHFBBLZJCLZZDBXGCXLQPXKFZFLSYLTUWBMQJHSZBMDDBCY");
            sbChineseFirstPY.Append(
                            "SCCLDXYCDDQLYJJWMQLLCSGLJJSYFPYYCCYLTJANTJJPWYCMMGQYYSXDXQMZHSZXPFTWWZQSWQRFKJLZJQQYFBRXJHHFWJJZYQAZ");
            sbChineseFirstPY.Append(
                            "MYFRHCYYBYQWLPEXCCZSTYRLTTDMQLYKMBBGMYYJPRKZNPBSXYXBHYZDJDNGHPMFSGMWFZMFQMMBCMZZCJJLCNUXYQLMLRYGQZCY");
            sbChineseFirstPY.Append(
                            "XZLWJGCJCGGMCJNFYZZJHYCPRRCMTZQZXHFQGTJXCCJEAQCRJYHPLQLSZDJRBCQHQDYRHYLYXJSYMHZYDWLDFRYHBPYDTSSCNWBX");
            sbChineseFirstPY.Append(
                            "GLPZMLZZTQSSCPJMXXYCSJYTYCGHYCJWYRXXLFEMWJNMKLLSWTXHYYYNCMMCWJDQDJZGLLJWJRKHPZGGFLCCSCZMCBLTBHBQJXQD");
            sbChineseFirstPY.Append(
                            "SPDJZZGKGLFQYWBZYZJLTSTDHQHCTCBCHFLQMPWDSHYYTQWCNZZJTLBYMBPDYYYXSQKXWYYFLXXNCWCXYPMAELYKKJMZZZBRXYYQ");
            sbChineseFirstPY.Append(
                            "JFLJPFHHHYTZZXSGQQMHSPGDZQWBWPJHZJDYSCQWZKTXXSQLZYYMYSDZGRXCKKUJLWPYSYSCSYZLRMLQSYLJXBCXTLWDQZPCYCYK");
            sbChineseFirstPY.Append(
                            "PPPNSXFYZJJRCEMHSZMSXLXGLRWGCSTLRSXBZGBZGZTCPLUJLSLYLYMTXMTZPALZXPXJTJWTCYYZLBLXBZLQMYLXPGHDSLSSDMXM");
            sbChineseFirstPY.Append(
                            "BDZZSXWHAMLCZCPJMCNHJYSNSYGCHSKQMZZQDLLKABLWJXSFMOCDXJRRLYQZKJMYBYQLYHETFJZFRFKSRYXFJTWDSXXSYSQJYSLY");
            sbChineseFirstPY.Append(
                            "XWJHSNLXYYXHBHAWHHJZXWMYLJCSSLKYDZTXBZSYFDXGXZJKHSXXYBSSXDPYNZWRPTQZCZENYGCXQFJYKJBZMLJCMQQXUOXSLYXX");
            sbChineseFirstPY.Append(
                            "LYLLJDZBTYMHPFSTTQQWLHOKYBLZZALZXQLHZWRRQHLSTMYPYXJJXMQSJFNBXYXYJXXYQYLTHYLQYFMLKLJTMLLHSZWKZHLJMLHL");
            sbChineseFirstPY.Append(
                            "JKLJSTLQXYLMBHHLNLZXQJHXCFXXLHYHJJGBYZZKBXSCQDJQDSUJZYYHZHHMGSXCSYMXFEBCQWWRBPYYJQTYZCYQYQQZYHMWFFHG");
            sbChineseFirstPY.Append(
                            "ZFRJFCDPXNTQYZPDYKHJLFRZXPPXZDBBGZQSTLGDGYLCQMLCHHMFYWLZYXKJLYPQHSYWMQQGQZMLZJNSQXJQSYJYCBEHSXFSZPXZ");
            sbChineseFirstPY.Append(
                            "WFLLBCYYJDYTDTHWZSFJMQQYJLMQXXLLDTTKHHYBFPWTYYSQQWNQWLGWDEBZWCMYGCULKJXTMXMYJSXHYBRWFYMWFRXYQMXYSZTZ");
            sbChineseFirstPY.Append(
                            "ZTFYKMLDHQDXWYYNLCRYJBLPSXCXYWLSPRRJWXHQYPHTYDNXHHMMYWYTZCSQMTSSCCDALWZTCPQPYJLLQZYJSWXMZZMMYLMXCLMX");
            sbChineseFirstPY.Append(
                            "CZMXMZSQTZPPQQBLPGXQZHFLJJHYTJSRXWZXSCCDLXTYJDCQJXSLQYCLZXLZZXMXQRJMHRHZJBHMFLJLMLCLQNLDXZLLLPYPSYJY");
            sbChineseFirstPY.Append(
                            "SXCQQDCMQJZZXHNPNXZMEKMXHYKYQLXSXTXJYYHWDCWDZHQYYBGYBCYSCFGPSJNZDYZZJZXRZRQJJYMCANYRJTLDPPYZBSTJKXXZ");
            sbChineseFirstPY.Append(
                            "YPFDWFGZZRPYMTNGXZQBYXNBUFNQKRJQZMJEGRZGYCLKXZDSKKNSXKCLJSPJYYZLQQJYBZSSQLLLKJXTBKTYLCCDDBLSPPFYLGYD");
            sbChineseFirstPY.Append(
                            "TZJYQGGKQTTFZXBDKTYYHYBBFYTYYBCLPDYTGDHRYRNJSPTCSNYJQHKLLLZSLYDXXWBCJQSPXBPJZJCJDZFFXXBRMLAZHCSNDLBJ");
            sbChineseFirstPY.Append(
                            "DSZBLPRZTSWSBXBCLLXXLZDJZSJPYLYXXYFTFFFBHJJXGBYXJPMMMPSSJZJMTLYZJXSWXTYLEDQPJMYGQZJGDJLQJWJQLLSJGJGY");
            sbChineseFirstPY.Append(
                            "GMSCLJJXDTYGJQJQJCJZCJGDZZSXQGSJGGCXHQXSNQLZZBXHSGZXCXYLJXYXYYDFQQJHJFXDHCTXJYRXYSQTJXYEFYYSSYYJXNCY");
            sbChineseFirstPY.Append(
                            "ZXFXMSYSZXYYSCHSHXZZZGZZZGFJDLTYLNPZGYJYZYYQZPBXQBDZTZCZYXXYHHSQXSHDHGQHJHGYWSZTMZMLHYXGEBTYLZKQWYTJ");
            sbChineseFirstPY.Append(
                            "ZRCLEKYSTDBCYKQQSAYXCJXWWGSBHJYZYDHCSJKQCXSWXFLTYNYZPZCCZJQTZWJQDZZZQZLJJXLSBHPYXXPSXSHHEZTXFPTLQYZZ");
            sbChineseFirstPY.Append(
                            "XHYTXNCFZYYHXGNXMYWXTZSJPTHHGYMXMXQZXTSBCZYJYXXTYYZYPCQLMMSZMJZZLLZXGXZAAJZYXJMZXWDXZSXZDZXLEYJJZQBH");
            sbChineseFirstPY.Append(
                            "ZWZZZQTZPSXZTDSXJJJZNYAZPHXYYSRNQDTHZHYYKYJHDZXZLSWCLYBZYECWCYCRYLCXNHZYDZYDYJDFRJJHTRSQTXYXJRJHOJYN");
            sbChineseFirstPY.Append(
                            "XELXSFSFJZGHPZSXZSZDZCQZBYYKLSGSJHCZSHDGQGXYZGXCHXZJWYQWGYHKSSEQZZNDZFKWYSSTCLZSTSYMCDHJXXYWEYXCZAYD");
            sbChineseFirstPY.Append(
                            "MPXMDSXYBSQMJMZJMTZQLPJYQZCGQHXJHHLXXHLHDLDJQCLDWBSXFZZYYSCHTYTYYBHECXHYKGJPXHHYZJFXHWHBDZFYZBCAPNPG");
            sbChineseFirstPY.Append(
                            "NYDMSXHMMMMAMYNBYJTMPXYYMCTHJBZYFCGTYHWPHFTWZZEZSBZEGPFMTSKFTYCMHFLLHGPZJXZJGZJYXZSBBQSCZZLZCCSTPGXM");
            sbChineseFirstPY.Append(
                            "JSFTCCZJZDJXCYBZLFCJSYZFGSZLYBCWZZBYZDZYPSWYJZXZBDSYUXLZZBZFYGCZXBZHZFTPBGZGEJBSTGKDMFHYZZJHZLLZZGJQ");
            sbChineseFirstPY.Append(
                            "ZLSFDJSSCBZGPDLFZFZSZYZYZSYGCXSNXXCHCZXTZZLJFZGQSQYXZJQDCCZTQCDXZJYQJQCHXZTDLGSCXZSYQJQTZWLQDQZTQCHQ");
            sbChineseFirstPY.Append(
                            "QJZYEZZZPBWKDJFCJPZTYPQYQTTYNLMBDKTJZPQZQZZFPZSBNJLGYJDXJDZZKZGQKXDLPZJTCJDQBXDJQJSTCKNXBXZMSLYJCQMT");
            sbChineseFirstPY.Append(
                            "JQWWCJQNJNLLLHJCWQTBZQYDZCZPZZDZYDDCYZZZCCJTTJFZDPRRTZTJDCQTQZDTJNPLZBCLLCTZSXKJZQZPZLBZRBTJDCXFCZDB");
            sbChineseFirstPY.Append(
                            "CCJJLTQQPLDCGZDBBZJCQDCJWYNLLZYZCCDWLLXWZLXRXNTQQCZXKQLSGDFQTDDGLRLAJJTKUYMKQLLTZYTDYYCZGJWYXDXFRSKS");
            sbChineseFirstPY.Append(
                            "TQTENQMRKQZHHQKDLDAZFKYPBGGPZREBZZYKZZSPEGJXGYKQZZZSLYSYYYZWFQZYLZZLZHWCHKYPQGNPGBLPLRRJYXCCSYYHSFZF");
            sbChineseFirstPY.Append(
                            "YBZYYTGZXYLXCZWXXZJZBLFFLGSKHYJZEYJHLPLLLLCZGXDRZELRHGKLZZYHZLYQSZZJZQLJZFLNBHGWLCZCFJYSPYXZLZLXGCCP");
            sbChineseFirstPY.Append(
                            "ZBLLCYBBBBUBBCBPCRNNZCZYRBFSRLDCGQYYQXYGMQZWTZYTYJXYFWTEHZZJYWLCCNTZYJJZDEDPZDZTSYQJHDYMBJNYJZLXTSST");
            sbChineseFirstPY.Append(
                            "PHNDJXXBYXQTZQDDTJTDYYTGWSCSZQFLSHLGLBCZPHDLYZJYCKWTYTYLBNYTSDSYCCTYSZYYEBHEXHQDTWNYGYCLXTSZYSTQMYGZ");
            sbChineseFirstPY.Append(
                            "AZCCSZZDSLZCLZRQXYYELJSBYMXSXZTEMBBLLYYLLYTDQYSHYMRQWKFKBFXNXSBYCHXBWJYHTQBPBSBWDZYLKGZSKYHXQZJXHXJX");
            sbChineseFirstPY.Append(
                            "GNLJKZLYYCDXLFYFGHLJGJYBXQLYBXQPQGZTZPLNCYPXDJYQYDYMRBESJYYHKXXSTMXRCZZYWXYQYBMCLLYZHQYZWQXDBXBZWZMS");
            sbChineseFirstPY.Append(
                            "LPDMYSKFMZKLZCYQYCZLQXFZZYDQZPZYGYJYZMZXDZFYFYTTQTZHGSPCZMLCCYTZXJCYTJMKSLPZHYSNZLLYTPZCTZZCKTXDHXXT");
            sbChineseFirstPY.Append(
                            "QCYFKSMQCCYYAZHTJPCYLZLYJBJXTPNYLJYYNRXSYLMMNXJSMYBCSYSYLZYLXJJQYLDZLPQBFZZBLFNDXQKCZFYWHGQMRDSXYCYT");
            sbChineseFirstPY.Append(
                            "XNQQJZYYPFZXDYZFPRXEJDGYQBXRCNFYYQPGHYJDYZXGRHTKYLNWDZNTSMPKLBTHBPYSZBZTJZSZZJTYYXZPHSSZZBZCZPTQFZMY");
            sbChineseFirstPY.Append(
                            "FLYPYBBJQXZMXXDJMTSYSKKBJZXHJCKLPSMKYJZCXTMLJYXRZZQSLXXQPYZXMKYXXXJCLJPRMYYGADYSKQLSNDHYZKQXZYZTCGHZ");
            sbChineseFirstPY.Append(
                            "TLMLWZYBWSYCTBHJHJFCWZTXWYTKZLXQSHLYJZJXTMPLPYCGLTBZZTLZJCYJGDTCLKLPLLQPJMZPAPXYZLKKTKDZCZZBNZDYDYQZ");
            sbChineseFirstPY.Append(
                            "JYJGMCTXLTGXSZLMLHBGLKFWNWZHDXUHLFMKYSLGXDTWWFRJEJZTZHYDXYKSHWFZCQSHKTMQQHTZHYMJDJSKHXZJZBZZXYMPAGQM");
            sbChineseFirstPY.Append(
                            "STPXLSKLZYNWRTSQLSZBPSPSGZWYHTLKSSSWHZZLYYTNXJGMJSZSUFWNLSOZTXGXLSAMMLBWLDSZYLAKQCQCTMYCFJBSLXCLZZCL");
            sbChineseFirstPY.Append(
                            "XXKSBZQCLHJPSQPLSXXCKSLNHPSFQQYTXYJZLQLDXZQJZDYYDJNZPTUZDSKJFSLJHYLZSQZLBTXYDGTQFDBYAZXDZHZJNHHQBYKN");
            sbChineseFirstPY.Append(
                            "XJJQCZMLLJZKSPLDYCLBBLXKLELXJLBQYCXJXGCNLCQPLZLZYJTZLJGYZDZPLTQCSXFDMNYCXGBTJDCZNBGBQYQJWGKFHTNPYQZQ");
            sbChineseFirstPY.Append(
                            "GBKPBBYZMTJDYTBLSQMPSXTBNPDXKLEMYYCJYNZCTLDYKZZXDDXHQSHDGMZSJYCCTAYRZLPYLTLKXSLZCGGEXCLFXLKJRTLQJAQZ");
            sbChineseFirstPY.Append(
                            "NCMBYDKKCXGLCZJZXJHPTDJJMZQYKQSECQZDSHHADMLZFMMZBGNTJNNLGBYJBRBTMLBYJDZXLCJLPLDLPCQDHLXZLYCBLCXZZJAD");
            sbChineseFirstPY.Append(
                            "JLNZMMSSSMYBHBSQKBHRSXXJMXSDZNZPXLGBRHWGGFCXGMSKLLTSJYYCQLTSKYWYYHYWXBXQYWPYWYKQLSQPTNTKHQCWDQKTWPXX");
            sbChineseFirstPY.Append(
                            "HCPTHTWUMSSYHBWCRWXHJMKMZNGWTMLKFGHKJYLSYYCXWHYECLQHKQHTTQKHFZLDXQWYZYYDESBPKYRZPJFYYZJCEQDZZDLATZBB");
            sbChineseFirstPY.Append(
                            "FJLLCXDLMJSSXEGYGSJQXCWBXSSZPDYZCXDNYXPPZYDLYJCZPLTXLSXYZYRXCYYYDYLWWNZSAHJSYQYHGYWWAXTJZDAXYSRLTDPS");
            sbChineseFirstPY.Append(
                            "SYYFNEJDXYZHLXLLLZQZSJNYQYQQXYJGHZGZCYJCHZLYCDSHWSHJZYJXCLLNXZJJYYXNFXMWFPYLCYLLABWDDHWDXJMCXZTZPMLQ");
            sbChineseFirstPY.Append(
                            "ZHSFHZYNZTLLDYWLSLXHYMMYLMBWWKYXYADTXYLLDJPYBPWUXJMWMLLSAFDLLYFLBHHHBQQLTZJCQJLDJTFFKMMMBYTHYGDCQRDD");
            sbChineseFirstPY.Append(
                            "WRQJXNBYSNWZDBYYTBJHPYBYTTJXAAHGQDQTMYSTQXKBTZPKJLZRBEQQSSMJJBDJOTGTBXPGBKTLHQXJJJCTHXQDWJLWRFWQGWSH");
            sbChineseFirstPY.Append(
                            "CKRYSWGFTGYGBXSDWDWRFHWYTJJXXXJYZYSLPYYYPAYXHYDQKXSHXYXGSKQHYWFDDDPPLCJLQQEEWXKSYYKDYPLTJTHKJLTCYYHH");
            sbChineseFirstPY.Append(
                            "JTTPLTZZCDLTHQKZXQYSTEEYWYYZYXXYYSTTJKLLPZMCYHQGXYHSRMBXPLLNQYDQHXSXXWGDQBSHYLLPJJJTHYJKYPPTHYYKTYEZ");
            sbChineseFirstPY.Append(
                            "YENMDSHLCRPQFDGFXZPSFTLJXXJBSWYYSKSFLXLPPLBBBLBSFXFYZBSJSSYLPBBFFFFSSCJDSTZSXZRYYSYFFSYZYZBJTBCTSBSD");
            sbChineseFirstPY.Append(
                            "HRTJJBYTCXYJEYLXCBNEBJDSYXYKGSJZBXBYTFZWGENYHHTHZHHXFWGCSTBGXKLSXYWMTMBYXJSTZSCDYQRCYTWXZFHMYMCXLZNS");
            sbChineseFirstPY.Append(
                            "DJTTTXRYCFYJSBSDYERXJLJXBBDEYNJGHXGCKGSCYMBLXJMSZNSKGXFBNBPTHFJAAFXYXFPXMYPQDTZCXZZPXRSYWZDLYBBKTYQP");
            sbChineseFirstPY.Append(
                            "QJPZYPZJZNJPZJLZZFYSBTTSLMPTZRTDXQSJEHBZYLZDHLJSQMLHTXTJECXSLZZSPKTLZKQQYFSYGYWPCPQFHQHYTQXZKRSGTTSQ");
            sbChineseFirstPY.Append(
                            "CZLPTXCDYYZXSQZSLXLZMYCPCQBZYXHBSXLZDLTCDXTYLZJYYZPZYZLTXJSJXHLPMYTXCQRBLZSSFJZZTNJYTXMYJHLHPPLCYXQJ");
            sbChineseFirstPY.Append(
                            "QQKZZSCPZKSWALQSBLCCZJSXGWWWYGYKTJBBZTDKHXHKGTGPBKQYSLPXPJCKBMLLXDZSTBKLGGQKQLSBKKTFXRMDKBFTPZFRTBBR");
            sbChineseFirstPY.Append(
                            "FERQGXYJPZSSTLBZTPSZQZSJDHLJQLZBPMSMMSXLQQNHKNBLRDDNXXDHDDJCYYGYLXGZLXSYGMQQGKHBPMXYXLYTQWLWGCPBMQXC");
            sbChineseFirstPY.Append(
                            "YZYDRJBHTDJYHQSHTMJSBYPLWHLZFFNYPMHXXHPLTBQPFBJWQDBYGPNZTPFZJGSDDTQSHZEAWZZYLLTYYBWJKXXGHLFKXDJTMSZS");
            sbChineseFirstPY.Append(
                            "QYNZGGSWQSPHTLSSKMCLZXYSZQZXNCJDQGZDLFNYKLJCJLLZLMZZNHYDSSHTHZZLZZBBHQZWWYCRZHLYQQJBEYFXXXWHSRXWQHWP");
            sbChineseFirstPY.Append(
                            "SLMSSKZTTYGYQQWRSLALHMJTQJSMXQBJJZJXZYZKXBYQXBJXSHZTSFJLXMXZXFGHKZSZGGYLCLSARJYHSLLLMZXELGLXYDJYTLFB");
            sbChineseFirstPY.Append(
                            "HBPNLYZFBBHPTGJKWETZHKJJXZXXGLLJLSTGSHJJYQLQZFKCGNNDJSSZFDBCTWWSEQFHQJBSAQTGYPQLBXBMMYWXGSLZHGLZGQYF");
            sbChineseFirstPY.Append(
                            "LZBYFZJFRYSFMBYZHQGFWZSYFYJJPHZBYYZFFWODGRLMFTWLBZGYCQXCDJYGZYYYYTYTYDWEGAZYHXJLZYYHLRMGRXXZCLHNELJJ");
            sbChineseFirstPY.Append(
                            "TJTPWJYBJJBXJJTJTEEKHWSLJPLPSFYZPQQBDLQJJTYYQLYZKDKSQJYYQZLDQTGJQYZJSUCMRYQTHTEJMFCTYHYPKMHYZWJDQFHY");
            sbChineseFirstPY.Append(
                            "YXWSHCTXRLJHQXHCCYYYJLTKTTYTMXGTCJTZAYYOCZLYLBSZYWJYTSJYHBYSHFJLYGJXXTMZYYLTXXYPZLXYJZYZYYPNHMYMDYYL");
            sbChineseFirstPY.Append(
                            "BLHLSYYQQLLNJJYMSOYQBZGDLYXYLCQYXTSZEGXHZGLHWBLJHEYXTWQMAKBPQCGYSHHEGQCMWYYWLJYJHYYZLLJJYLHZYHMGSLJL");
            sbChineseFirstPY.Append(
                            "JXCJJYCLYCJPCPZJZJMMYLCQLNQLJQJSXYJMLSZLJQLYCMMHCFMMFPQQMFYLQMCFFQMMMMHMZNFHHJGTTHHKHSLNCHHYQDXTMMQD");
            sbChineseFirstPY.Append(
                            "CYZYXYQMYQYLTDCYYYZAZZCYMZYDLZFFFMMYCQZWZZMABTBYZTDMNZZGGDFTYPCGQYTTSSFFWFDTZQSSYSTWXJHXYTSXXYLBYQHW");
            sbChineseFirstPY.Append(
                            "WKXHZXWZNNZZJZJJQJCCCHYYXBZXZCYZTLLCQXYNJYCYYCYNZZQYYYEWYCZDCJYCCHYJLBTZYYCQWMPWPYMLGKDLDLGKQQBGYCHJ");
            sbChineseFirstPY.Append("XY");

            string strChineseFirstPY = sbChineseFirstPY.ToString();

            StringBuilder resultString = new StringBuilder();
            int index = 0;
            foreach (char vChar in strText)
            {
                // 若是字母则直接输出,否则输出拼音首字母
                if ((vChar >= 'a' && vChar <= 'z') || (vChar >= 'A' && vChar <= 'Z') || (vChar.ToString().Contains("_"))
                    || (vChar.ToString().Contains("(")) || (vChar.ToString().Contains(")")) || (vChar.ToString().Contains("*"))
                    || (vChar.ToString().Contains("&")) || (vChar.ToString().Contains("^")) || (vChar.ToString().Contains("%"))
                    || (vChar.ToString().Contains("$")) || (vChar.ToString().Contains("#")) || (vChar.ToString().Contains("@"))
                    || (vChar.ToString().Contains("!")) || (vChar.ToString().Contains("`")) || (vChar.ToString().Contains("~"))
                    || (vChar.ToString().Contains("<")) || (vChar.ToString().Contains(">")) || (vChar.ToString().Contains("/"))
                    || (vChar.ToString().Contains(".")) || (vChar.ToString().Contains("/")) || (vChar.ToString().Contains("\\"))
                    || (vChar.ToString().Contains("+")) || (vChar.ToString().Contains("=")) || (vChar.ToString().Contains("-"))
                    || (vChar.ToString().Contains("（")) || (vChar.ToString().Contains("）")) || (vChar.ToString().Contains("《"))
                    || (vChar.ToString().Contains("》")) || (vChar.ToString().Contains("？")) || (vChar.ToString().Contains("！"))
                    || (vChar.ToString().Contains("“")) || (vChar.ToString().Contains("”")) || (vChar.ToString().Contains("："))
                    || (vChar.ToString().Contains("；")) || (vChar.ToString().Contains(";")) || (vChar.ToString().Contains(":"))
                    || (vChar.ToString().Contains("?")) || (vChar.ToString().Contains("[")) || (vChar.ToString().Contains("]"))
                    || (vChar.ToString().Contains("{")) || (vChar.ToString().Contains("}")) || (vChar.ToString().Contains("|"))
                    || (vChar.ToString().Contains("｛")) || (vChar.ToString().Contains("｝")) || (vChar.ToString().Contains("【"))
                    || (vChar.ToString().Contains("】")) || (vChar.ToString().Contains("——")) || (vChar.ToString().Contains("—"))
                    || (vChar >= '0' && vChar <= '9'))
                {
                    resultString.Append(char.ToUpper(vChar));
                }
                else
                {
                    index = (int)vChar - 19968;
                    if (index >= 0 && index < strChineseFirstPY.Length)
                    {
                        resultString.Append(strChineseFirstPY[index]);
                    }
                }
            } //foreach
            return resultString.ToString();
        }

        /// <summary>
        /// 得到字符串的值（如果为空串，则返回默认值）
        /// </summary>
        /// <param name="value">指定字符串</param>
        /// <param name="defaultValue">为空时指定默认值</param>
        /// <returns>字符串</returns>
        public static string GetStringValue(string value, string defaultValue = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            return value;
        }
    }
}