using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;

namespace WinDo.Utilities
{
    /// <summary>
    /// FTP帮助类
    /// </summary>
    public class FTPHelper
    {


        #region 测试ftp服务
        private static ManualResetEvent timeoutObject;
        private static Socket socket = null;
        private static bool isConn = false;
        /// <summary>
        /// 通过socket判断ftp是否通畅(异步socket连接,同步发送接收数据)
        /// </summary> 
        /// <returns></returns>
        public static bool CheckFtp(string ip, string ftpuser, string ftppas, out string errmsg, int port = 21, int timeout = 2000)
        {
            #region 输入数据检查
            if (ftpuser.Trim().Length == 0)
            {
                errmsg = "FTP用户名不能为空,请检查设置!";
                return false;
            }
            if (ftppas.Trim().Length == 0)
            {
                errmsg = "FTP密码不能为空,请检查设置!";
                return false;
            }
            IPAddress address;
            try
            {
                address = IPAddress.Parse(ip);
            }
            catch
            {
                errmsg = string.Format("FTP服务器IP:{0}解析失败,请检查是否设置正确!", ip);
                return false;
            }
            #endregion
            isConn = false;

            bool ret = false;
            byte[] result = new byte[1024];
            int pingStatus = 0, userStatus = 0, pasStatus = 0, exitStatus = 0; //连接返回,用户名返回,密码返回,退出返回
            timeoutObject = new ManualResetEvent(false);
            try
            {
                int receiveLength;

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SendTimeout = timeout;
                socket.ReceiveTimeout = timeout;//超时设置成2000毫秒

                try
                {
                    socket.BeginConnect(new IPEndPoint(address, port), new AsyncCallback(callBackMethod), socket); //开始异步连接请求
                    if (!timeoutObject.WaitOne(timeout, false))
                    {
                        socket.Close();
                        socket = null;
                        pingStatus = -1;
                    }
                    if (isConn)
                    {
                        pingStatus = 200;
                    }
                    else
                    {
                        pingStatus = -1;
                    }
                }
                catch (Exception ex)
                {
                    pingStatus = -1;
                }

                if (pingStatus == 200) //状态码200 - TCP连接成功
                {
                    receiveLength = socket.Receive(result);
                    pingStatus = getFtpReturnCode(result, receiveLength); //连接状态
                    if (pingStatus == 220)//状态码220 - FTP返回欢迎语
                    {
                        socket.Send(Encoding.Default.GetBytes(string.Format("{0}{1}", "USER " + ftpuser, Environment.NewLine)));
                        receiveLength = socket.Receive(result);
                        userStatus = getFtpReturnCode(result, receiveLength);
                        if (userStatus == 331)//状态码331 - 要求输入密码
                        {
                            socket.Send(Encoding.Default.GetBytes(string.Format("{0}{1}", "PASS " + ftppas, Environment.NewLine)));
                            receiveLength = socket.Receive(result);
                            pasStatus = getFtpReturnCode(result, receiveLength);
                            if (pasStatus == 230)//状态码230 - 登入因特网
                            {
                                errmsg = string.Format("FTP:{0}@{1}登陆成功", ip, port);
                                ret = true;
                                socket.Send(Encoding.Default.GetBytes(string.Format("{0}{1}", "QUIT", Environment.NewLine))); //登出FTP
                                receiveLength = socket.Receive(result);
                                exitStatus = getFtpReturnCode(result, receiveLength);
                            }
                            else
                            { // 状态码230的错误
                                errmsg = string.Format("FTP:{0}@{1}登陆失败,用户名或密码错误({2})", ip, port, pasStatus);
                            }
                        }
                        else
                        {// 状态码331的错误 
                            errmsg = string.Format("使用用户名:'{0}'登陆FTP:{1}@{2}时发生错误({3}),请检查FTP是否正常配置!", ftpuser, ip, port, userStatus);
                        }
                    }
                    else
                    {// 状态码220的错误 
                        errmsg = string.Format("FTP:{0}@{1}返回状态错误({2}),请检查FTP服务是否正常运行!", ip, port, pingStatus);
                    }
                }
                else
                {// 状态码200的错误
                    errmsg = string.Format("无法连接FTP服务器:{0}@{1},请检查FTP服务是否启动!", ip, port);
                }
            }
            catch (Exception ex)
            { //连接出错 
                errmsg = string.Format("FTP:{0}@{1}连接出错:", ip, port) + ex.Message;
                WinDo.Utilities.LogHelper.WriteException(ex);
                ret = false;
            }
            finally
            {
                if (socket != null)
                {
                    socket.Close(); //关闭socket
                    socket = null;
                }
            }
            return ret;
        }
        private static void callBackMethod(IAsyncResult asyncResult)
        {
            try
            {
                socket = asyncResult.AsyncState as Socket;
                if (socket != null)
                {
                    socket.EndConnect(asyncResult);
                    isConn = true;
                }
            }
            catch (Exception ex)
            {
                isConn = false;
            }
            finally
            {
                timeoutObject.Set();
            }
        }
        /// <summary>
        /// 传递FTP返回的byte数组和长度,返回状态码(int)
        /// </summary>
        /// <param name="retByte"></param>
        /// <param name="retLen"></param>
        /// <returns></returns>
        private static int getFtpReturnCode(byte[] retByte, int retLen)
        {
            try
            {
                string str = Encoding.ASCII.GetString(retByte, 0, retLen).Trim();
                return int.Parse(str.Substring(0, 3));
            }
            catch
            {
                return -1;
            }
        }
        #endregion


        #region 字段
        string ftpURI;
        string ftpUserID;
        string ftpServerIP;
        string ftpPassword;
        string ftpRemotePath;
        #endregion

        /// <summary>  
        /// 连接FTP服务器
        /// </summary>  
        /// <param name="FtpServerIP">FTP连接地址</param>  
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>  
        /// <param name="FtpUserID">用户名</param>  
        /// <param name="FtpPassword">密码</param>  
        public FTPHelper(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            if (FtpServerIP.Contains("ftp://"))
            {
                ftpURI =  ftpServerIP + "/" + ftpRemotePath + "/";
            }
            else
            {
                ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
            }
            
        }

        /// <summary>  
        /// 上传  
        /// </summary>   
        public  void Upload(string Localfilename,string RemoteFileName="")
        {
            var fileInf = new FileInfo(Localfilename);
            FtpWebRequest reqFTP;
            if (RemoteFileName == "")
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileInf.Name));
            }
            else
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + RemoteFileName));
            }
            

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.KeepAlive = false;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool MakeDir(string directory, string user_name, string password)
        {

            try
            {
                //create the directory
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(user_name, password);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    return false;
                }
            }
        }


        public static void UploadImage(Image image, string to_uri, string user_name, string password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(to_uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user_name, password);
            byte[] bytes = FileHelper.ImageToByte(image);
            request.ContentLength = bytes.Length;
            using (Stream request_stream = request.GetRequestStream())
            {
                request_stream.Write(bytes, 0, bytes.Length);
                request_stream.Close();
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名（带扩展名）</param>
        public void Download(string filePath, string fileName)
        {
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 删除文件  
        /// </summary>  
        public void Delete(string fileName)
        {
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.KeepAlive = false;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取当前目录下明细(包含文件和文件夹)  
        /// </summary>  
        public string[] GetFilesDetailList()
        {
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取FTP文件列表(包括文件夹)
        /// </summary>   
        private string[] GetAllList(string url)
        {
            List<string> list = new List<string>();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(new Uri(url));
            req.Credentials = new NetworkCredential(ftpPassword, ftpPassword);
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            req.UseBinary = true;
            req.UsePassive = true;
            try
            {
                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return list.ToArray();
        }

        /// <summary>  
        /// 获取当前目录下文件列表(不包括文件夹)  
        /// </summary>  
        public string[] GetFileList(string url)
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpPassword, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {

                    if (line.IndexOf("<DIR>") == -1)
                    {
                        result.Append(Regex.Match(line, @"[\S]+ [\S]+", RegexOptions.IgnoreCase).Value.Split(' ')[1]);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result.ToString().Split('\n');
        }

        /// <summary>  
        /// 判断当前目录下指定的文件是否存在  
        /// </summary>  
        /// <param name="RemoteFileName">远程文件名</param>  
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>  
        /// 创建文件夹  
        /// </summary>   
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>  
        /// 获取指定文件大小  
        /// </summary>  
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            { }
            return fileSize;
        }

        /// <summary>  
        /// 更改文件名  
        /// </summary> 
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            { }
        }

        /// <summary>  
        /// 移动文件  
        /// </summary>  
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        }

        /// <summary>  
        /// 切换当前目录  
        /// </summary>  
        /// <param name="IsRoot">true:绝对路径 false:相对路径</param>   
        public void GotoDirectory(string DirectoryName, bool IsRoot)
        {
            if (IsRoot)
            {
                ftpRemotePath = DirectoryName;
            }
            else
            {
                ftpRemotePath += DirectoryName + "/";
            }
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }
    }
}
