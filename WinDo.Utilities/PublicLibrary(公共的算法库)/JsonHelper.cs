using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WinDo.Utilities
{
    public class JsonHelper
    {


        /// <summary>
        /// 把json转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 对像转换为Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Json串转换为对像
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object JsonToObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }

        public static T JsonToObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static void SaveJsonData(string fileName, object obj)
        {
            var path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Mock", $"{fileName}.json");
            System.IO.File.WriteAllText(path, JsonHelper.ObjectToJson(obj));
        }
        public static T LoadJsonData<T>(string fileName) where T : new()
        {
            var path = Path.Combine(System.Windows.Forms.Application.StartupPath, "Mock", $"{fileName}.json");
            if (!File.Exists(path))
            {
                return new T();
            }
            var json = File.ReadAllText(path);
            return JsonToObject<T>(json);
        }



        #region 将json转换为DataTable
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名  
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名  
            //strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            //strJson = strJson.Substring(0, strJson.IndexOf("]"));
            //获取数据  
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');
                //创建表  
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = "ECGVALUE";
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');
                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容  
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        #endregion

        #region Json 字符串 转换为 DataTable数据集合
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        #endregion
        /// <summary>
        /// Json转Hashtable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Hashtable Json2Hashtable(string json)
        {
            return JsonConvert.DeserializeObject<Hashtable>(json);
        }

        public static string ConvertDataTableToJson(DataTable dtRisInfo, int nIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int j = 0; j < dtRisInfo.Columns.Count; j++)
            {
                sb.Append("\"" + dtRisInfo.Columns[j].ColumnName + "\":");
                if (j == dtRisInfo.Columns.Count - 1)
                {
                    if (dtRisInfo.Rows[nIndex][dtRisInfo.Columns[j].ColumnName].ToString() == "")
                    {
                        sb.Append("null");
                    }
                    else
                    {
                        sb.Append("\"" + dtRisInfo.Rows[nIndex][dtRisInfo.Columns[j].ColumnName].ToString() + "\"");
                    }
                }
                else
                {
                    if (dtRisInfo.Rows[nIndex][dtRisInfo.Columns[j].ColumnName].ToString() == "")
                    {
                        sb.Append("null,");
                    }
                    else
                    {
                        sb.Append("\"" + dtRisInfo.Rows[nIndex][dtRisInfo.Columns[j].ColumnName].ToString() + "\",");
                    }
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
