using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace WinDo.Utilities
{
    public class RequestWebServiceHelper
    {

        public static string CallServiceURL;


        public static dynamic CallWS(string op, string paraName, string input, string syncDesc, Action<string> _failedAction = null)
        {
            if (string.IsNullOrWhiteSpace(CallServiceURL) && _failedAction != null)
            {
                _failedAction(syncDesc + " 失败，" + "未配置服务地址");
                return null;
            }

            try
            {
                HttpWebRequest request = CreateWebRequest(CallServiceURL + "?op=" + op);

                var xmldoc = System.Xml.Linq.XDocument.Parse(string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
<{0} xmlns=""http://tempuri.org/"">
      <{1}>{2}</{1}>
    </{0}>
  </soap:Body>
</soap:Envelope>", op, paraName, input));
                using (Stream stream = request.GetRequestStream())
                {
                    xmldoc.Save(stream);
                }
                string soapResult = "";
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }
                var rsxml = System.Xml.Linq.XDocument.Parse(soapResult).Descendants().FirstOrDefault(i => i.Name.LocalName == op + "Result");
                if (rsxml != null)
                {
                    dynamic rsobj = WinDo.Utilities.JsonHelper.JsonToObject(rsxml.Value);
                    return rsobj;
                }
            }
            catch (Exception ex)
            {
                if (_failedAction != null)
                    _failedAction(syncDesc + " 失败，" + ex.Message);
            }
            return null;
        }



        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest(string URL)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URL);
            webRequest.Proxy = null;//不使用代理
            webRequest.KeepAlive = false;//不建立持久性连接
            webRequest.ContentType = "text/xml;charset=utf-8";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Timeout = 20000;
            return webRequest;
        }

    }
}
