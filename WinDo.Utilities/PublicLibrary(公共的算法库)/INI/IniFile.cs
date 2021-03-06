using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinDo.Utilities
{
	/// <summary>
	/// 包含一组方法和属性，提供对 ini 文件的基本操作。
	/// </summary>
	[ComVisible(true)]
	[Serializable]
    public class IniFile
    {
        public string path;

        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);


        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, this.path);
            return temp;
        }
        /// <summary>
        /// 读取ini文件的某段落下所有键名
        /// </summary>    
        public string[] IniReadValues(string Section)
        {
            byte[] sectionByte = IniReadValues(Section, null);
            return ByteToString(sectionByte);
        }
        /// <summary>
        /// 读取ini文件的所有段落名
        /// </summary>    
        public string[] IniReadValues()
        {
            byte[] allSection = IniReadValues(null, null);
            return ByteToString(allSection);

        }
        /// <summary>
        /// 转换byte[]类型为string[]数组类型 
        /// </summary>
        /// <param name="sectionByte"></param>
        /// <returns></returns>
        private string[] ByteToString(byte[] sectionByte)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            //编码所有key的string类型
            string sections = ascii.GetString(sectionByte);
            //获取key的数组
            string[] sectionList = sections.Split(new char[1] { '\0' });
            return sectionList;
        }
        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }
        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        public void ClearSection(string Section)
        {
            IniWriteValue(Section, null, null);
        }
    }
}