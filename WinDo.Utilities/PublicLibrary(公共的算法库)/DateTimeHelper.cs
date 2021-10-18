
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace WinDo.Utilities
{
    /// <summary>
    /// 日期时间公共处理类   
    /// </summary>
    public class DateTimeHelper
    {
        private static ChineseLunisolarCalendar chineseData = new ChineseLunisolarCalendar();

        internal static CultureInfo cnci = new CultureInfo("zh-cn");



        /// <summary>
        /// 获取固定间隔的所有分钟范围
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static List<Tuple<DateTime, int>> MinutesRange(DateTime fromDate, DateTime toDate, int minutes)
        {
            if (toDate <= fromDate)
                return new List<Tuple<DateTime, int>>();
            var totalMinutes = toDate.Subtract(fromDate).TotalMinutes.AsInt();

            //var count = Math.Ceiling((totalMinutes.AsDecimal() / minutes));
            var count = (minutes == 0 ? 0 : Math.Ceiling((totalMinutes.AsDecimal() / minutes)));
            var rs = Enumerable.Range(0, count.AsInt()).Select(d =>
            {
                var min = minutes;
                return new Tuple<DateTime, int>(fromDate.AddMinutes(minutes * d), min);
            }).ToList();
            //var lastDur = (totalMinutes % minutes);
            var lastDur = (minutes == 0 ? 0 : (totalMinutes % minutes));
            if (rs.Count > 0 && lastDur > 0)
            {
                rs[rs.Count - 1] = new Tuple<DateTime, int>(rs.Last().Item1, lastDur);
            }
            return rs;
        }


        #region 获取格式化的日期时间
        /// <summary>
        /// 返回默认格式化的日期(格式化字符串:yyyy-MM-dd)
        /// </summary>
        /// <param name="dt">要格式化的时间</param>
        /// <returns>格式化后的时间</returns>
        public static string GetDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(DateTime date, string hms)
        {
            return (date.ToString("yyyy-MM-dd") + " " + hms).AsDateTime();
        }

        /// <summary>
        /// 获取到分钟的 yyyy-MM-dd HH:mm 字符串 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDateMinute(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 返回默认格式化的时间(格式化字符串:HH:mm:ss)
        /// </summary>
        /// <param name="dt">要格式化的时间</param>
        /// <returns>格式化后的时间</returns>
        public static string GetTime(DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回默认格式化的日期和时间(格式化字符串:yyyy-MM-dd HH:mm:ss)
        /// </summary>
        /// <param name="dt">要格式化的时间</param>
        /// <returns>格式化后的时间</returns>
        public static string GetDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回使用指定格式化串的日期/时间
        /// </summary>
        /// <param name="dt">要格式化的时间</param>
        /// <param name="format">格式化时间使用的字符串</param>
        /// <returns>格式化后的时间</returns>
        public static string GetFormatTime(DateTime dt, string format)
        {
            return dt.ToString(format);
        }
        #endregion

        /// <summary>
        /// 判断输入是否为日期类型
        /// </summary>
        /// <param name="strValue">待检查数据</param>
        /// <returns></returns>
        public static bool IsDate(string strValue)
        {
            try
            {
                DateTime date = DateTime.Parse(strValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将输入的字符串转换成日期时间型数据
        /// </summary>
        /// <param name="strValue">待转换数据</param>
        /// <returns>成功，返回日期时间型数据；失败，返回null</returns>
        public static DateTime? ToDataTime(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return DateTime.Parse(strValue.Trim());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将输入的字符串转换成指定格式（"yyyy-MM-dd")日期型数据
        /// </summary>
        /// <param name="strValue">待转换数据</param>
        /// <returns>成功，返回日期型数据；失败，返回null</returns>
        public static DateTime? ToDate(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return DateTime.Parse(DateTime.Parse(strValue.Trim()).ToString("yyyy-MM-dd"));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将输入的日期字符串转换成指定格式（"yyyy-MM-dd")字符型日期数据
        /// </summary>
        /// <param name="s">待转换数据</param>
        /// <returns>成功，返回字符型日期数据；失败，返回""</returns>
        public static string FormatDate(string strValue)
        {
            if (IsDate(strValue.Trim()))
            {
                return DateTime.Parse(strValue.Trim()).ToString("yyyy-MM-dd");
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// 得到指定月份的天数
        /// </summary>
        /// <param name="strYear">年份</param>
        /// <param name="strMonth">月份</param>
        /// <returns>返回该年度下的该月份的天数</returns>
        public static string GetNumberOfDays(string strYear, string strMonth)
        {
            string sDayCount = string.Empty;

            int iMonth = Convert.ToInt32(strMonth);

            int iYear = Convert.ToInt32(strYear);
            strMonth = Convert.ToString(iMonth);

            switch (strMonth)
            {
                case "1":
                    sDayCount = "31";
                    break;

                case "2":
                    //闰年的计算方法 是一百的倍数就看能否除于400 否则 判断这个数是否能整除4 且不能整除100
                    if ((iYear % 400 == 0) || ((iYear % 100 != 0) && (iYear % 4 == 0)))
                    {
                        sDayCount = "29";
                    }
                    else
                    {
                        sDayCount = "28";
                    }

                    break;

                case "3":
                    sDayCount = "31";
                    break;

                case "4":
                    sDayCount = "30";
                    break;

                case "5":
                    sDayCount = "31";
                    break;

                case "6":
                    sDayCount = "30";
                    break;

                case "7":
                    sDayCount = "31";
                    break;

                case "8":
                    sDayCount = "31";
                    break;

                case "9":
                    sDayCount = "30";
                    break;

                case "10":
                    sDayCount = "31";
                    break;

                case "11":
                    sDayCount = "30";
                    break;

                case "12":
                    sDayCount = "31";
                    break;
            }

            return sDayCount;
        }

        public static void GetAge(DateTime dtBirthday, DateTime dtNow, out string Age, out string AgeUnit)
        {
            string strAge = string.Empty;                         // 年龄的字符串表示
            int intYear = 0;                                    // 岁
            int intMonth = 0;                                    // 月
            int intDay = 0;                                    // 天
            Age = "0";
            AgeUnit = "";

            // 计算天数
            intDay = dtNow.Day - dtBirthday.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }

            // 计算月数
            intMonth = dtNow.Month - dtBirthday.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }

            // 计算年数
            intYear = dtNow.Year - dtBirthday.Year;

            // 格式化年龄输出
            if (intYear >= 1)                                            // 年份输出
            {
                Age = intYear.ToString();
                AgeUnit = "岁";
            }
            else if (intMonth > 0)                           // 月
            {
                Age = intMonth.ToString();
                AgeUnit = "月";
            }
            else if (intDay >= 0)                              // 天数
            {
                Age = intDay.ToString();
                AgeUnit = "天";

            }


        }

        /// <summary>当前日期的星期名称</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(DateTime dt)
        {
            string week = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }


        /// <summary>返回当前日期的星期编号</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期数字编号</returns>
        public static int GetWeekNumberOfDay(DateTime dt)
        {
            int week = 0;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = 1;
                    break;
                case DayOfWeek.Tuesday:
                    week = 2;
                    break;
                case DayOfWeek.Wednesday:
                    week = 3;
                    break;
                case DayOfWeek.Thursday:
                    week = 4;
                    break;
                case DayOfWeek.Friday:
                    week = 5;
                    break;
                case DayOfWeek.Saturday:
                    week = 6;
                    break;
                case DayOfWeek.Sunday:
                    week = 7;
                    break;
            }
            return week;
        }

        /// <summary>
        /// 获取某一年有多少周
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>该年周数</returns>
        public static int GetWeekAmount(int year)
        {
            var end = new DateTime(year, 12, 31); //该年最后一天
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //该年星期数
        }

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>该日期在该年中的周数</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public static void WeekRange(int year, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            //当年的第一天
            var firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;

            firstDate = firstDay.AddDays(dayDiff).Date;
            lastDate = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays(addDays);
                lastDate = lastDate.AddDays(addDays);
            }
        }

        /// <summary>
        /// 返回两个日期之间相差的天数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>天数</returns>
        public static int DiffDays(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto.Date - dtfrm.Date;
            return tsDiffer.Days;
        }

        /// <summary>
        /// 将日期对象转化为格式字符串
        /// </summary>
        /// <param name="oDateTime">日期对象</param>
        /// <param name="strFormat">
        /// 格式：
        ///		"SHORTDATE"===短日期
        ///		"LONGDATE"==长日期
        ///		其它====自定义格式
        /// </param>
        /// <returns>日期字符串</returns>
        public static string ToString(DateTime oDateTime, string strFormat)
        {
            string strDate;

            try
            {
                switch (strFormat.ToUpper())
                {
                    case "SHORTDATE":
                        strDate = oDateTime.ToShortDateString();
                        break;
                    case "LONGDATE":
                        strDate = oDateTime.ToLongDateString();
                        break;
                    default:
                        strDate = oDateTime.ToString(strFormat);
                        break;
                }
            }
            catch (Exception)
            {
                strDate = oDateTime.ToShortDateString();
            }

            return strDate;
        }


        /*
        //1.1 取当前年月日时分秒 
                currentTime=System.DateTime.Now;
        //1.2 取当前年 
                int 年=currentTime.Year; 
        //1.3 取当前月 
                int 月=currentTime.Month; 
        //1.4 取当前日 
                int 日=currentTime.Day; 
        //1.5 取当前时 
                int 时=currentTime.Hour; 
        //1.6 取当前分 
                int 分=currentTime.Minute; 
        //1.7 取当前秒 
                int 秒=currentTime.Second; 
        //1.8 取当前毫秒 
                int 毫秒=currentTime.Millisecond; 
        //（变量可用中文） 

        //1.9 取中文日期显示——年月日时分 
                         string strY=currentTime.ToString("f"); //不显示秒 

        //1.10 取中文日期显示_年月 
                 string strYM=currentTime.ToString("y"); 

        //1.11 取中文日期显示_月日 
                 string strMD=currentTime.ToString("m"); 

        //1.12 取中文年月日 
                 string strYMD=currentTime.ToString("D"); 

        /1.13 取当前时分，格式为：14：24 
        string strT=currentTime.ToString("t"); 

        //1.14 取当前时间，格式为：2003-09-23T14:46:48 
        string strT=currentTime.ToString("s"); 

        //1.15 取当前时间，格式为：2003-09-23 14:48:30Z 
                                              string strT=currentTime.ToString("u"); 

        //1.16 取当前时间，格式为：2003-09-23 14:48 
        string strT=currentTime.ToString("g"); 

        //1.17 取当前时间，格式为：Tue, 23 Sep 2003 14:52:40 GMT 
                                                     string strT=currentTime.ToString("r"); 

        //1.18获得当前时间 n 天后的日期时间 
            DateTime newDay = DateTime.Now.AddDays(100); 
        */

        /// <summary>返回本年有多少天</summary>
        /// <param name="iYear">年份</param>
        /// <returns>本年的天数</returns>
        public static int GetDaysOfYear(int iYear)
        {
            return IsRuYear(iYear) ? 366 : 365;
        }


        /// <summary>本年有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>本天在当年的天数</returns>
        public static int GetDaysOfYear(DateTime dt)
        {
            return IsRuYear(dt.Year) ? 366 : 365;
        }


        /// <summary>本月有多少天</summary>
        /// <param name="iYear">年</param>
        /// <param name="Month">月</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(int iYear, int Month)
        {
            var days = 0;
            switch (Month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(iYear) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }

            return days;
        }


        /// <summary>本月有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(DateTime dt)
        {
            //--------------------------------//
            //--从dt中取得当前的年，月信息  --//
            //--------------------------------//
            int days = 0;
            int year = dt.Year;
            int month = dt.Month;

            //--利用年月信息，得到当前月的天数信息。
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(year) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }
            return days;
        }

        /// <summary>
        /// 判断是否为合法日期，必须大于1800年1月1日
        /// </summary>
        /// <param name="dateTime">输入日期字符串</param>
        /// <returns>True/False</returns>
        public static bool IsDateTime(string dateTime)
        {
            if (dateTime == null)
            {
                return false;
            }
            else
            {
                try
                {
                    DateTime d = DateTime.Parse(dateTime);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            //return ValidateUtil.IsDateTime(dateTime);
        }

        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="iYear">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(int iYear)
        {
            //形式参数为年份
            //例如：2003
            int n = iYear;
            return (n % 400 == 0) || (n % 4 == 0 && n % 100 != 0);
        }

        /// <summary>
        /// 获取某个指定日期的生肖
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static AnimalSign GetAnimalSign(DateTime dt)
        {
            if (IsLunisolarSupported(dt.Year))
            {
                return (AnimalSign)chineseData.GetTerrestrialBranch(chineseData.GetSexagenaryYear(dt));
            }
            else
            {
                throw new Exception("不支持给定的时间");
            }
        }

        #region 星座相关
        /// <summary>
        /// 根据输入的日期返回对应的星座
        /// </summary>
        /// <param name="dt">要查询的日期</param>
        /// <returns>查询的日期对应的星座</returns>
        public Constellation GetConstellation(DateTime dt)
        {
            int calc = dt.Month * 100 + dt.Day;

            if ((calc >= 321) && (calc <= 419))
            {
                return Constellation.白羊座;
            }

            else if ((calc >= 420) && (calc <= 520))
            {
                return Constellation.金牛座;
            }
            else if ((calc >= 521) && (calc <= 620))
            {
                return Constellation.双子座;
            }

            else if ((calc >= 621) && (calc <= 722))
            {
                return Constellation.巨蟹座;
            }

            else if ((calc >= 723) && (calc <= 822))
            {
                return Constellation.狮子座;
            }

            else if ((calc >= 823) && (calc <= 922))
            {
                return Constellation.处女座;
            }

            else if ((calc >= 923) && (calc <= 1023))
            {
                return Constellation.天秤座;
            }

            else if ((calc >= 1024) && (calc <= 1122))
            {
                return Constellation.天蝎座;
            }

            else if ((calc >= 1123) && (calc <= 1221))
            {
                return Constellation.射手座;
            }

            else if ((calc >= 1222) || (calc <= 119))
            {
                return Constellation.摩羯座;
            }

            else if ((calc >= 120) && (calc <= 218))
            {
                return Constellation.水瓶座;
            }

            else { return Constellation.双鱼座; }
        }
        #endregion

        /// <summary>
        /// 检查农历是否支持给定的年份
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsLunisolarSupported(int year)
        {
            if (year > 1901 && year < 2101)
            {
                return true;
            }
            return false;

        }
    }

    /// <summary>
    /// 星座列表
    /// </summary>
    public enum Constellation
    {
        /// <summary>
        /// 水瓶座01月20日~02月18日
        /// </summary>
        水瓶座,

        /// <summary>
        /// 双鱼座02月19日~03月20日
        /// </summary>
        双鱼座,

        /// <summary>
        /// 白羊座03月21日~04月19日
        /// </summary>
        白羊座,

        /// <summary>
        /// 金牛座04月20日~05月20日
        /// </summary>
        金牛座,

        /// <summary>
        /// 双子座05月21日~06月21日
        /// </summary>
        双子座,

        /// <summary>
        /// 巨蟹座06月22日~07月22日
        /// </summary>
        巨蟹座,

        /// <summary>
        /// 狮子座07月23日~08月22日
        /// </summary>
        狮子座,

        /// <summary>
        /// 处女座08月23日~09月22日
        /// </summary>
        处女座,

        /// <summary>
        /// 天秤座09月23日~10月23日
        /// </summary>
        天秤座,

        /// <summary>
        /// 天蝎座10月24日~11月22日
        /// </summary>
        天蝎座,

        /// <summary>
        /// 射手座11月23日~12月21日
        /// </summary>
        射手座,

        /// <summary>
        /// 魔羯座12月22日~01月19日
        /// </summary>
        摩羯座
    }

    /// <summary>
    /// 12生肖
    /// </summary>
    public enum AnimalSign
    {
        /// <summary>
        /// 鼠
        /// </summary>
        鼠 = 1,

        /// <summary>
        /// 牛
        /// </summary>
        牛 = 2,

        /// <summary>
        /// 虎
        /// </summary>
        虎 = 3,

        /// <summary>
        /// 兔
        /// </summary>
        兔 = 4,

        /// <summary>
        /// 龙
        /// </summary>
        龙 = 5,

        /// <summary>
        /// 蛇
        /// </summary>
        蛇 = 6,

        /// <summary>
        /// 马
        /// </summary>
        马 = 7,

        /// <summary>
        /// 羊
        /// </summary>
        羊 = 8,

        /// <summary>
        /// 猴
        /// </summary>
        猴 = 9,

        /// <summary>
        /// 鸡
        /// </summary>
        鸡 = 10,

        /// <summary>
        /// 狗
        /// </summary>
        狗 = 11,

        /// <summary>
        /// 猪
        /// </summary>
        猪 = 12
    }

    /// <summary>
    /// 12地支
    /// </summary>
    public enum EarthlyBranch
    {
        /// <summary>
        /// 子
        /// </summary>
        子,

        /// <summary>
        /// 丑
        /// </summary>
        丑,

        /// <summary>
        /// 寅
        /// </summary>
        寅,

        /// <summary>
        /// 卯
        /// </summary>
        卯,

        /// <summary>
        /// 辰
        /// </summary>
        辰,

        /// <summary>
        /// 巳
        /// </summary>
        巳,

        /// <summary>
        /// 午
        /// </summary>
        午,

        /// <summary>
        /// 未
        /// </summary>
        未,

        /// <summary>
        /// 申
        /// </summary>
        申,

        /// <summary>
        /// 酉
        /// </summary>
        酉,

        /// <summary>
        /// 戌
        /// </summary>
        戌,

        /// <summary>
        /// 亥
        /// </summary>
        亥
    }

    /// <summary>
    /// 天干
    /// </summary>
    public enum HeavenlyStem
    {
        /// <summary>
        /// 甲
        /// </summary>
        甲,

        /// <summary>
        /// 乙
        /// </summary>
        乙,

        /// <summary>
        /// 丙
        /// </summary>
        丙,

        /// <summary>
        /// 丁
        /// </summary>
        丁,

        /// <summary>
        /// 戊
        /// </summary>
        戊,

        /// <summary>
        /// 己
        /// </summary>
        己,

        /// <summary>
        /// 庚
        /// </summary>
        庚,

        /// <summary>
        /// 辛
        /// </summary>
        辛,

        /// <summary>
        /// 壬
        /// </summary>
        壬,

        /// <summary>
        /// 癸
        /// </summary>
        癸
    }

    /// <summary>
    /// 24个节气
    /// </summary>
    public enum SolarTerms
    {
        /// <summary>
        /// 立春
        /// </summary>
        立春,

        /// <summary>
        /// 雨水
        /// </summary>
        雨水,

        /// <summary>
        /// 惊蛰
        /// </summary>
        惊蛰,

        /// <summary>
        /// 春分
        /// </summary>
        春分,

        /// <summary>
        /// 清明
        /// </summary>
        清明,

        /// <summary>
        /// 谷雨
        /// </summary>
        谷雨,

        /// <summary>
        /// 立夏
        /// </summary>
        立夏,

        /// <summary>
        /// 小满
        /// </summary>
        小满,

        /// <summary>
        /// 芒种
        /// </summary>
        芒种,

        /// <summary>
        /// 夏至
        /// </summary>
        夏至,

        /// <summary>
        /// 小暑
        /// </summary>
        小暑,

        /// <summary>
        /// 大暑
        /// </summary>
        大暑,

        /// <summary>
        /// 立秋
        /// </summary>
        立秋,

        /// <summary>
        /// 处暑
        /// </summary>
        处暑,

        /// <summary>
        /// 白露
        /// </summary>
        白露,

        /// <summary>
        /// 秋分
        /// </summary>
        秋分,

        /// <summary>
        /// 寒露
        /// </summary>
        寒露,

        /// <summary>
        /// 霜降
        /// </summary>
        霜降,

        /// <summary>
        /// 立冬
        /// </summary>
        立冬,

        /// <summary>
        /// 小雪
        /// </summary>
        小雪,
        /// <summary>
        /// 大雪
        /// </summary>
        大雪,

        /// <summary>
        /// 冬至
        /// </summary>
        冬至,

        /// <summary>
        /// 小寒
        /// </summary>
        小寒,

        /// <summary>
        /// 大寒
        /// </summary>
        大寒
    }

    /// <summary>
    /// 中文的星期列表
    /// </summary>
    public enum DayOfWeekCn
    {
        /// <summary>
        /// 星期日
        /// </summary>
        星期日 = 0,

        /// <summary>
        /// 星期一
        /// </summary>
        星期一 = 1,

        /// <summary>
        /// 星期二
        /// </summary>
        星期二 = 2,

        /// <summary>
        /// 
        /// </summary>
        星期三 = 3,

        /// <summary>
        /// 星期四
        /// </summary>
        星期四 = 4,

        /// <summary>
        /// 星期五
        /// </summary>
        星期五 = 5,

        /// <summary>
        /// 星期六
        /// </summary>
        星期六 = 6

    }
}