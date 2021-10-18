using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace WinDo.Utilities
{
    /// <summary>
    /// 通用文件处理类   
    /// </summary>
    public class FileHelper : IDisposable
    {
        private bool _alreadyDispose = false;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public FileHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        ~FileHelper()
        {
            Dispose(); ;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            //if (isDisposing)
            //{
            //     if (xml != null)
            //     {
            //         xml = null;
            //     }
            //}
            _alreadyDispose = true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region public static byte[] GetFile(string fileName):读取文件
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>字节</returns>
        public static byte[] GetFile(string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var binaryReader = new BinaryReader(fileStream);
            var file = binaryReader.ReadBytes(((int)fileStream.Length));
            binaryReader.Close();
            fileStream.Close();
            return file;
        }
        #endregion

        #region public static void SaveFile(byte[] file, string fileName):保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="fileName">文件名</param>
        public static void SaveFile(byte[] file, string fileName)
        {
            var directoryName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            var fileStream = new FileStream(fileName, FileMode.Create);
            fileStream.Write(file, 0, file.Length);
            fileStream.Close();
        }
        #endregion

        /// <summary>
        /// Image Type To Byte
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns>byte[]</returns>
        public static byte[] ImageToByte(Image image, System.Drawing.Imaging.ImageFormat imgFormat = null)
        {
            if (imgFormat == null)
                imgFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, imgFormat);
            var file = memoryStream.GetBuffer();
            memoryStream.Close();
            return file;
        }

        /// <summary>
        /// Byte To Image
        /// </summary>
        /// <param name="buffer">byte</param>
        /// <returns>Image</returns>
        public static Image ByteToImage(byte[] buffer)
        {
            var memoryStream = new MemoryStream();
            memoryStream = new MemoryStream(buffer);
            var image = Image.FromStream(memoryStream);
            memoryStream.Close();
            return image;
        }

        /// <summary>
        /// 递归创建文件夹
        /// 采用了递归的方式，可以把不存在的父文件夹都创建好，然后再创建好指定的文件夹。
        /// </summary>
        /// <example>
        /// CreateDirectory(@"c:\test1\test2\test3\test4");
        /// </example>
        /// <param name="directoryName">文件夹的路径</param>
        public static void CreateDirectory(string directoryName)
        {
            var sParentDirectory = Path.GetDirectoryName(directoryName);
            if (!Directory.Exists(sParentDirectory))
                CreateDirectory(sParentDirectory);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// 判断指定文件是否存在
        /// </summary>
        /// <param name="filePath">文件所在的路径</param>
        /// <returns></returns>
        public static bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        #region string GetPostfixStr(string filename):取得文件后缀名
        /// <summary>
        /// 取得文件后缀名
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns>.gif|.html格式</returns>
        /// <example>
        /// string filename = "aaa.aspx";
        /// string s = FileHelper.GetPostfixStr(filename); 
        /// </example>
        public static string GetPostfixStr(string filename)
        {
            var start = filename.LastIndexOf(".");
            var length = filename.Length;
            var postfix = filename.Substring(start, length - start);
            return postfix;
        }
        #endregion



        #region long GetFileSize(string fileName):得到当前文件的大小(以字节为单位)
        /// <summary>
        /// 得到当前文件的大小(以字节为单位)
        /// </summary>
        /// <param name="fileName">文件的完全限定名或相对限定名</param>
        /// <returns>当前文件的大小(以字节为单位)</returns>
        /// <example>
        /// string filename = "C:\\a.jpg"; 
        /// long lSize = GetFileSize(filename);
        /// </example>
        public static long GetFileSize(string fileName)
        {
            var fInfo = new FileInfo(fileName);
            return fInfo.Length;
        }
        #endregion

        #region void WriteFile(string Path, string Strings):写文件,会覆盖掉以前的内容
        /// <summary>
        /// 写文件,会覆盖掉以前的内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="strings">文件内容</param>
        /// <example>
        /// string Path = Server.MapPath("Default2.aspx"); 
        /// string Strings = "没是测试内容。";
        /// FileHelper.WriteFile(Path,Strings);
        /// </example>
        public static void WriteFile(string path, string strings)
        {
            if (!System.IO.File.Exists(path))
            {
                var directoryPath = path.Substring(0, path.LastIndexOf("\\"));

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var f = System.IO.File.Create(path);
                f.Close();
            }
            var f2 = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("gb2312"));
            f2.Write(strings);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region string ReadFile(string Path):读取文本内容
        /// <summary>
        /// 读取文本内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回读取的文本内容</returns>
        /// <example>
        /// string Path = Server.MapPath("Default2.aspx");
        /// string content = FileHelper.ReadFile(Path);
        /// </example>
        public static string ReadFile(string path)
        {
            var value = "";
            if (!System.IO.File.Exists(path))
                value = "不存在相应的目录";
            else
            {
                using (var f2 = new StreamReader(path, System.Text.Encoding.GetEncoding("gb2312")))
                {
                    value = f2.ReadToEnd();
                }
            }
            return value;
        }
        #endregion

        #region void FileAdd(string Path, string strings):追加文件内容
        /// <summary>
        /// 追加文件内容
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="strings">内容</param>
        /// <example>
        /// string Path = Server.MapPath("Default2.aspx");
        /// string Strings = "新追加内容";
        /// FileHelper.FileAdd(Path, Strings);
        /// </example>
        public static void FileAdd(string path, string strings)
        {
            var sw = File.AppendText(path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region void FileCoppy(string orignFile, string NewFile):拷贝文件
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="orignFile">原始文件</param>
        /// <param name="newFile">新文件路径</param>
        /// <example>
        /// string orignFile = Server.MapPath("Default2.aspx");     
        /// string NewFile = Server.MapPath("Default3.aspx");
        /// FileHelper.FileCoppy(OrignFile, NewFile);
        /// </example>
        public static void FileCoppy(string orignFile, string newFile)
        {
            File.Copy(orignFile, newFile, true);
        }

        #endregion

        #region void FileDel(string Path):删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <example>
        ///  string Path = Server.MapPath("Default3.aspx"); 
        /// FileHelper.FileDel(Path);
        /// </example>
        public static void FileDel(string path)
        {
            File.Delete(path);
        }
        #endregion

        #region void FileMove(string orignFile, string NewFile):移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="orignFile">原始路径</param>
        /// <param name="newFile">新路径</param>
        /// <example>
        /// string orignFile = Server.MapPath("../说明.txt");   
        /// string NewFile = Server.MapPath("../../说明.txt");
        /// FileHelper.FileMove(OrignFile, NewFile);
        /// </example>
        public static void FileMove(string orignFile, string newFile)
        {
            File.Move(orignFile, newFile);
        }
        #endregion

        #region void FolderCreate(string orignFolder, string NewFloder):在当前目录下创建目录
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="orignFolder">当前目录</param>
        /// <param name="newFloder">新目录</param>
        /// <example>
        /// string orignFolder = Server.MapPath("test/");  
        /// string NewFloder = "new";
        /// FileHelper.FolderCreate(OrignFolder, NewFloder); 
        /// </example>
        public static void FolderCreate(string orignFolder, string newFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(newFloder);
        }
        #endregion

        #region void DeleteFolder(string dir):递归删除文件夹目录及文件
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <example>
        /// string dir = Server.MapPath("test/");  
        ///  FileHelper.DeleteFolder(dir);  
        /// </example>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (var d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 
            }

        }

        #endregion

        #region void CopyDir(string srcPath, string aimPath):将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
        /// <summary>
        /// 将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        /// <example>
        /// string srcPath = Server.MapPath("test/");  
        /// string aimPath = Server.MapPath("test1/");
        /// FileHelper.CopyDir(srcPath,aimPath);   
        /// </example>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                var fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (var file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        File.Copy(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        #endregion

        #region void GetFile(string fileAllName, out string filePath, out string fileName, out string fileType):得到全部
        /// <summary>
        /// 得到全部
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <param name="filePath">返回文件路径</param>
        /// <param name="fileName">返回不带反缀名的文件名</param>
        /// <param name="fileType">返回带.的后缀名</param>
        public static void GetFile(string fileAllName, out string filePath, out string fileName, out string fileType)
        {
            filePath = fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + "\\";//在私有变量里存储源文件路径
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));//在私有变量里存储源文件类型
        }
        #endregion

        #region void GetName(string fileAllName, out string fileName):得到文件名
        /// <summary>
        /// 得到文件名
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <param name="fileName">返回不带反缀名的文件名</param>
        public static void GetName(string fileAllName, out string fileName)//得到文件名
        {
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
        }
        #endregion

        #region public static string GetName(string fileAllName):得到文件名
        /// <summary>
        /// 得到文件名
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <returns>文件名</returns>
        public static string GetName(string fileAllName)
        {
            return fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\') - 1);
        }
        #endregion

        #region void GetType(string fileAllName, out string fileType):得到文件类型
        /// <summary>
        /// 得到文件类型
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <param name="fileType">返回带.的后缀名</param>
        public static void GetType(string fileAllName, out string fileType)//得到文件类型
        {
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));//在私有变量里存储源文件类型
        }
        #endregion

        #region string GetType(string fileAllName):得到文件类型
        /// <summary>
        /// 得到文件类型
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <returns></returns>
        public static string GetType(string fileAllName)
        {
            return fileAllName.Substring(fileAllName.LastIndexOf('.'));//在私有变量里存储源文件类型
        }
        #endregion

        #region void GetPath(string fileAllName, out string filePath):得到文件路径
        /// <summary>
        /// 得到文件路径
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <param name="filePath">返回文件路径</param>
        public static void GetPath(string fileAllName, out string filePath)//得到文件路径
        {
            filePath = fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + "\\";//在私有变量里存储源文件路径
        }
        #endregion

        #region string GetPath(string fileAllName):得到文件路径
        /// <summary>
        /// 得到文件路径
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <returns>文件路径</returns>
        public static string GetPath(string fileAllName)//得到文件路径
        {
            return fileAllName.Substring(0, fileAllName.LastIndexOf('\\')) + "\\";
        }
        #endregion

        #region void GetNameAndType(string fileAllName, out string fileName, out string fileType):得到文件名和类型
        /// <summary>
        /// 得到文件名和类型
        /// </summary>
        /// <param name="fileAllName">带文件名和后缀的全路径</param>
        /// <param name="fileName">返回不带反缀名的文件名</param>
        /// <param name="fileType">返回带.的后缀名</param>
        public static void GetNameAndType(string fileAllName, out string fileName, out string fileType)//得到文件名和类型
        {
            fileName = fileAllName.Substring(fileAllName.LastIndexOf('\\') + 1, fileAllName.LastIndexOf('.') - fileAllName.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
            fileType = fileAllName.Substring(fileAllName.LastIndexOf('.'));//在私有变量里存储源文件类型
        }
        #endregion

        #region string GetDirectoryName(string directory):得到目录名
        /// <summary>
        /// 得到目录名
        /// </summary>
        /// <param name="directory">目录总路径</param>
        /// <returns>返回目录名</returns>
        public static string GetDirectoryName(string directory)//得到目录名
        {
            string outDirectory;
            if (directory.Length - 1 == directory.LastIndexOf('\\'))//如果是根目录
            {
                outDirectory = directory.Substring(0, directory.LastIndexOf(":")) + "盘";//如果是D盘，则返回D
            }
            else
            {
                outDirectory = directory.Substring(directory.LastIndexOf('\\') + 1);//在私有变量里存储源文件名
            }
            return outDirectory;
        }
        #endregion
    }
}