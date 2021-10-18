using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
    /// <summary>
    /// 操作INI文件公共类   
    /// </summary>
    public class IniFileHelper
    {
        #region 外部导入函数

        //对ini文件进行写操作的函数 
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        protected static extern long WritePrivateProfileString(string section, string key, string val, string filePath); 

        //对ini文件进行读操作的函数
        [DllImport("kernel32", CharSet = CharSet.Auto)]
        protected static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath); 

        #endregion

        private string fileName = string.Empty;  //用于存放ini文件的路径和名称
        private Hashtable sections; //用于存放整个ini文件的内容

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        /// <summary>
        /// IniFile的构造函数
        /// </summary>
        /// <param name="fileName">ini文件的路径和名称</param>
        public IniFileHelper(string strFileName)
        {
            fileName = strFileName;
            sections = new Hashtable();
            LoadValues();
        }

        //~IniFile()
        //{
        //    sections = null;
        //}

        /// <summary>
        /// 装载Ini文件的内容
        /// </summary>
        private void LoadValues()
        {
            bool aa = File.Exists(fileName);
            if ((fileName.Trim() != "") && (File.Exists(fileName)))
            {
                //GetEncoding("gb2312") 访止读取中文时出现乱码
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.GetEncoding("gb2312")))
                {
                    SetStrings(sr);
                }
            }
        }
        /// <summary>
        /// 添加一个ini文件的Section
        /// </summary>
        /// <param name="item">section的名字</param>
        /// <returns>添加的section</returns>
        private Hashtable AddSection(string item)
        {
            Hashtable sectionsItem = new Hashtable();
            sections.Add(item, sectionsItem);
            return sectionsItem;
        }


        /// <summary>
        /// 将ini文件的内容读取到内存当中
        /// </summary>
        /// <param name="sr">读取文件的流</param>
        private void SetStrings(StreamReader sr)
        {
            String line;
            Hashtable sectionsKeys = null;
            int splitPos;
            string keyName, keyValue;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();

                if (line == "") continue;

                if (line.Substring(0, 1) != ";")    //表示ini文件中的注释行
                {
                    if ((line.Substring(0, 1) == "[") && (line.Substring(line.Length - 1, 1) == "]"))
                    {
                        line = line.Substring(1, line.Length - 2);
                        sectionsKeys = AddSection(line.Trim());
                    }

                    else
                    {
                        splitPos = line.IndexOf('=');
                        if ((splitPos > 0) && (sectionsKeys != null))
                        {
                            keyName = line.Substring(0, splitPos).Trim();

                            if (keyName.Length == 0)
                            {
                                throw new Exception("IniFile Syntax Error!");
                            }

                            if (keyName.Length < line.Length - 1)
                            {
                                keyValue = line.Substring(splitPos + 1, line.Length - 1 - splitPos);
                            }
                            else
                            {
                                keyValue = "";
                            }
                            sectionsKeys.Add(keyName, keyValue);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 将内容写回到Ini文件当中
        /// </summary>
        public void UpdateFile()
        {
            Hashtable sectionsItem;
            using (StreamWriter sw = new StreamWriter(fileName))
            {

                foreach (DictionaryEntry scnItem in sections)
                {
                    sw.WriteLine("[" + (string)scnItem.Key + "]");

                    sectionsItem = (Hashtable)sections[(string)scnItem.Key];
                    foreach (DictionaryEntry keyItem in sectionsItem)
                    {
                        sw.WriteLine((string)keyItem.Key + "=" + (string)keyItem.Value);
                    }

                    sw.WriteLine("");
                }

                sw.Close();
            }

        }


        /// <summary>
        /// 读取Ini文件所有的section
        /// </summary>
        /// <returns>以ArrayList形式返回以字符串表示的section</returns>
        public ArrayList ReadSections()
        {
            ArrayList sectionList = new ArrayList();

            foreach (DictionaryEntry item in sections)
            {
                sectionList.Add((string)item.Key);
            }
            return sectionList;
        }


        /// <summary>
        /// 读取Ini文件的一个section中的所有key
        /// </summary>
        /// <param name="sectionName">要读取的section</param>
        /// <returns>以ArrayList形式返回以字符串表示的kry</returns>
        public ArrayList ReadKeys(string sectionName)
        {
            ArrayList keyList = new ArrayList();

            Hashtable sectionsItem = (Hashtable)sections[sectionName.Trim()];

            foreach (DictionaryEntry item in sectionsItem)
            {
                keyList.Add((string)item.Key);
            }

            return keyList;
        }


        /// <summary>
        /// 判断一个section是否存在
        /// </summary>
        /// <param name="sectionName">要检查的section的名字</param>
        /// <returns>如果section存在,则返回true,否则返回false</returns>
        public bool SectionExists(string sectionName)
        {
            return sections.ContainsKey(sectionName.Trim());
        }


        /// <summary>
        /// 判断一个key在一个section中是否存在
        /// </summary>
        /// <param name="sectionName">所指定的section</param>
        /// <param name="keyName">所要检查的key的名字</param>
        /// <returns>如果此key在这个section中存在,则返回true,否则返回false</returns>
        public bool KeyExists(string sectionName, string keyName)
        {
            sectionName = sectionName.Trim();

            if (!sections.ContainsKey(sectionName))
            {
                return false;
            }

            return ((Hashtable)sections[sectionName]).ContainsKey(keyName.Trim());
        }

        /// <summary>
        /// 删除一个section
        /// </summary>
        /// <param name="sectionName">所要删除的section的名字</param>
        public void EraseSection(string sectionName)
        {
            sections.Remove(sectionName);
        }


        /// <summary>
        ///在一个sectionk中删除一个key
        /// </summary>
        /// <param name="sectionName">所指定的section</param>
        /// <param name="keyName">所要删除的key的名字</param>
        public void DeleteKey(string sectionName, string keyName)
        {
            sectionName = sectionName.Trim();

            if (!sections.ContainsKey(sectionName))
            {
                return;
            }

            ((Hashtable)sections[sectionName]).Remove(keyName.Trim());
        }

        /// <summary>
        /// 读取键的值. 
        /// </summary>
        /// <param name="sectionName">节点名</param>
        /// <param name="keyName">键名</param>
        /// <param name="defaultValue">默认的值,这个值是用户给的.</param>
        /// <returns>在ini文件中没有找到节点名或键值则返回 默认值,找到了则返回ini文件中的值.</returns>
        public string ReadString(string sectionName, string keyName, string defaultValue = "")
        {
            sectionName = sectionName.Trim();
            keyName = keyName.Trim();

            if (!sections.ContainsKey(sectionName))
            {
                return defaultValue;
            }

            Hashtable sectionsItem = (Hashtable)sections[sectionName];
            if (!sectionsItem.ContainsKey(keyName))
            {
                return defaultValue;
            }
            string aa = (string)sectionsItem[keyName];
            return (string)sectionsItem[keyName];

        }
        /// <summary>
        /// 添加sectionName 和键值
        /// </summary>
        /// <param name="sectionName">ini文件的节点</param>
        /// <param name="keyName">ini文件的键名</param>
        /// <param name="stringValue">ini文件的值</param>
        public void WriteString(string sectionName, string keyName, string stringValue)
        {
            Hashtable sectionsItem;
            // sections
            sectionName = sectionName.Trim();
            keyName = keyName.Trim();
            stringValue = stringValue.Trim();
            ////
            //sections.ContainsKey(sectionName.Trim());

            bool xx = SectionExists(sectionName);

            //if (!sections.ContainsKey(sectionName))
            if (!SectionExists(sectionName))
            {
                sectionsItem = AddSection(sectionName);
            }
            else
            {
                sectionsItem = (Hashtable)sections[sectionName];
            }

            //if (!sectionsItem.ContainsKey(keyName))
            if (sectionsItem.ContainsKey(keyName))
            {
                sectionsItem[keyName] = stringValue;
            }
            else
            {
                sectionsItem.Add(keyName, stringValue);
            }
        }

        //*********************************************************************************

        public long ReadInteger(string sectionName, string keyName, long defaultValue)
        {
            return Convert.ToInt64(ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public void WriteInteger(string sectionName, string keyName, long longValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(longValue));
        }



        public bool ReadBool(string sectionName, string keyName, bool defaultValue)
        {
            return Convert.ToBoolean(ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }

        public void WriteBool(string sectionName, string keyName, bool boolValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(boolValue));
        }


        public int ReadBinaryStream(string sectionName, string keyName, Stream defaultValue)
        {
            return 0;
        }
        public void WriteBinaryStream(string sectionName, string keyName, Stream streamValue)
        { }

        public DateTime ReadDate(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(ReadString(sectionName, keyName, Convert.ToString(defaultValue))).Date;
        }
        public void WriteDate(string sectionName, string keyName, DateTime dateValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(dateValue.Date));
        }



        public DateTime ReadDateTime(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }
        public void WriteDateTime(string sectionName, string keyName, DateTime dateTimeValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(dateTimeValue));
        }


        public double ReadFloat(string sectionName, string keyName, double defaultValue)
        {
            return Convert.ToDouble(ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }
        public void WriteFloat(string sectionName, string keyName, double doubleValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(doubleValue));
        }


        public DateTime ReadTime(string sectionName, string keyName, DateTime defaultValue)
        {
            return Convert.ToDateTime(ReadString(sectionName, keyName, Convert.ToString(defaultValue)));
        }
        public void WriteTime(string sectionName, string keyName, DateTime timeValue)
        {
            WriteString(sectionName, keyName, Convert.ToString(timeValue.TimeOfDay));
        }
    }
}
