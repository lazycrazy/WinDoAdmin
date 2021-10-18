














using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace WinDoControls
{





    public static class FontImages
    {
        private static readonly PrivateFontCollection m_fontCollection = new PrivateFontCollection();
        private static readonly Dictionary<string, Font> m_fontsAwesome = new Dictionary<string, Font>();
        private static readonly Dictionary<string, Font> m_fontsElegant = new Dictionary<string, Font>();
        private static readonly Dictionary<string, Font> m_fontsIconfont = new Dictionary<string, Font>();

        private static Dictionary<int, float> m_cacheMaxSize = new Dictionary<int, float>();

        private const int MinFontSize = 8;
        private const int MaxFontSize = 200;

        static FontImages()
        {
            LoadFont("iconfont");
            LoadFont("FontAwesome");
            LoadFont("ElegantIcons");

            float size = MinFontSize;
            var fm = FontIconfont;
            for (int i = 0; i <= (MaxFontSize - MinFontSize) * 2; i++)
            {
                m_fontsIconfont.Add(size.ToString("F2"), new Font(fm, size, FontStyle.Regular, GraphicsUnit.Pixel));
                size += 0.5f;
            }
        }

        private static void LoadFont(string fileName)
        {
            string strPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.ToLower().Replace("file:///", "");
            string strDir = System.IO.Path.GetDirectoryName(strPath);
            if (!Directory.Exists(Path.Combine(strDir, "IconFont")))
            {
                Directory.CreateDirectory(Path.Combine(strDir, "IconFont"));
            }
            string strFile = Path.Combine(strDir, string.Format("IconFont\\{0}.ttf", fileName));
            if (!File.Exists(strFile))
            {
                var fs = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("WinDoControls.IconFont.{0}.ttf", fileName));
                FileStream sw = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                fs.CopyTo(sw);
                sw.Close();
                fs.Close();
            }

            m_fontCollection.AddFontFile(strFile);
        }

        public static FontFamily FontAwesome
        {
            get
            {
                return GetFontFamily("FontAwesome");
            }
        }

        public static FontFamily FontIconfont
        {
            get
            {
                return GetFontFamily("iconfont");
            }
        }

        public static FontFamily FontElegantIcons
        {
            get
            {
                return GetFontFamily("ElegantIcons");
            }
        }
        private static FontFamily GetFontFamily(string name)
        {
            for (int i = 0; i < m_fontCollection.Families.Length; i++)
            {
                if (m_fontCollection.Families[i].Name == name)
                {
                    return m_fontCollection.Families[i];
                }
            }
            return m_fontCollection.Families[0];
        }
        static Graphics TestGraphics = Graphics.FromImage(new Bitmap(10, 10));
        static SolidBrush brush2 = new SolidBrush(Color.Black);
        public static Bitmap GetImage(FontIcons iconText, int imageSize = 20, Color? foreColor = null, Color? backColor = null)
        {
            var fm = FontIconfont;
            if (iconText.ToString().StartsWith("A_"))
                fm = FontAwesome;
            else if (iconText.ToString().StartsWith("E_"))
                fm = FontElegantIcons;
            var fontSize = imageSize;//* ((float)14 / 20);
            var imageFont = new Font(fm, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            string s = char.ConvertFromUtf32((int)iconText);
            var textSize = TestGraphics.MeasureString(s, imageFont).ToSize();
            Bitmap srcImage = new Bitmap(textSize.Width, textSize.Height);
            using (Graphics graphics = Graphics.FromImage(srcImage))
            {
                if (backColor.HasValue && backColor.Value != Color.Empty && backColor.Value != Color.Transparent)
                    graphics.Clear(backColor.Value);
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SetGDIHigh();
                if (!foreColor.HasValue)
                    foreColor = Color.Black;
                brush2.Color = foreColor.Value;
                graphics.DrawString(s, imageFont, brush2, new PointF());
            }

            return srcImage;
        }


        private static Icon ToIcon(Bitmap srcBitmap, int size)
        {
            if (srcBitmap == null)
            {
                throw new ArgumentNullException("srcBitmap");
            }

            Icon icon;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new Bitmap(srcBitmap, new Size(size, size)).Save(memoryStream, ImageFormat.Png);
                Stream stream = new MemoryStream();
                BinaryWriter binaryWriter = new BinaryWriter(stream);
                if (stream.Length <= 0L)
                {
                    return null;
                }

                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)size);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((byte)0);
                binaryWriter.Write((short)0);
                binaryWriter.Write((short)32);
                binaryWriter.Write((int)memoryStream.Length);
                binaryWriter.Write(22);
                binaryWriter.Write(memoryStream.ToArray());
                binaryWriter.Flush();
                binaryWriter.Seek(0, SeekOrigin.Begin);
                icon = new Icon(stream);
                stream.Dispose();
            }

            return icon;
        }
    }
}