using System;
using System.IO;

namespace WinDo.Utilities
{
    /// <summary>
    /// 写日志文件公共类  
    /// </summary>
    public class LogHelper
    {
        private static readonly object writeFile = new object();
        private static StreamWriter streamWriter; //写文件  
        public static void WriteException(Exception exception)
        {
            WriteLog(exception);
        }
        public static string LogDirectoryPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Log");

        /// <summary>
        /// 在本地写入错误日志
        /// </summary>
        /// <param name="exception"></param> 错误信息
        public static void WriteLog(Exception exception)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = LogDirectoryPath;
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }

                    if (exception != null)
                    {
                        streamWriter.WriteLine(dt.ToString("HH:mm:ss") + "  异常信息：" + exception.ToString());
                    }
                }
                catch (Exception)
                { }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

        /// <summary>
        /// 在本地写入调试日志
        /// </summary>
        /// <param name="strInfo"></param> 调试信息
        public static void WriteLog(string strMethodName, string strInfo)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = LogDirectoryPath;
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }

                    if (!string.IsNullOrEmpty(strInfo))
                    {
                        streamWriter.WriteLine(dt.ToString("HH:mm:ss") + "  方法：" + strMethodName + "  描述：" + strInfo);
                    }
                }
                catch (Exception)
                { }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }
        /// <summary>
        /// 在本地写入调试日志
        /// </summary>
        /// <param name="strInfo"></param> 调试信息
        public static void WriteHourLog(string strMethodName, string strInfo)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = LogDirectoryPath;
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += "\\" + dt.ToString("yyyy-MM-dd"); //每天一个目录
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }

                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd-HH"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }

                    if (!string.IsNullOrEmpty(strInfo))
                    {
                        streamWriter.WriteLine("");
                        streamWriter.WriteLine(dt.ToString("HH:mm:ss fff") + "  方法：" + strMethodName + "  描述：" + strInfo);
                    }
                }
                catch (Exception)
                { }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

        private static void InitLog(string filPath)
        {
            streamWriter = !File.Exists(filPath) ? File.CreateText(filPath) : File.AppendText(filPath);
        }
    }

}